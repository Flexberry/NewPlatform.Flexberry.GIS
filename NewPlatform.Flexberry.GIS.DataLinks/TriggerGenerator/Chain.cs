namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ICSSoft.STORMNET;

    public class Chain
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType">Тип объекта</param>
        /// <param name="prefix"></param>
        /// <param name="parentAlias">Алиас предыдущего свойства</param>
        /// <param name="index">Индекс свойства в цепочке</param>
        public Chain(Type objectType, string prefix, string parentAlias = "", int index = 0)
        {
            DeclaredType = objectType;
            PrimaryKey = Information.GetPrimaryKeyStorageName(objectType);
            TableName = Information.GetClassStorageName(objectType);
            BasePrefix = prefix;
            Alias = $"{parentAlias}_{index}";
        }

        public Dictionary<string, Chain> Joins = new Dictionary<string, Chain>();

        /// <summary>
        /// Тип хранимого объекта
        /// </summary>
        public Type DeclaredType { get; private set; }

        /// <summary>
        /// Таблица текущего класса
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// Таблица текущего класса
        /// </summary>
        public string FullTableName
        {
            get { return BasePrefix + TableName; }
        }

        /// <summary>
        /// Префикс таблицы
        /// </summary>
        public string BasePrefix { get; private set; }

        /// <summary>
        /// Поле первичного ключа по которому дочерние таблицы связываются с данной
        /// </summary>
        public string PrimaryKey { get; private set; }
        
        /// <summary>
        /// Алиас в запросе
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Поля, при изменении которых надо прогонять триггер
        /// </summary>
        public List<string> LookForUpdateFields = new List<string>();

        private List<string> GetDataBaseStorageNames(Type propertyType, string propertyName)
        {
            var databaseFieldNames = new List<string>();
            if (propertyType.IsSubclassOf(typeof(DataObject)))
            {
                var typeUsage = new TypeUsage();
                var usageTypes = typeUsage.GetUsageTypes(DeclaredType, propertyName);

                for (var i = 0; i < usageTypes.Length; i++)
                {
                    databaseFieldNames.Add(Information.GetPropertyStorageName(DeclaredType, propertyName, i));
                }
            }
            else
            {
                databaseFieldNames.Add(Information.GetPropertyStorageName(DeclaredType, propertyName));
            }

            return databaseFieldNames;
        }

        /// <summary>
        /// Добавляет элемент в цепочку
        /// </summary>
        /// <param name="chain"></param>
        public void Add(string chain)
        {
            if (chain.Contains('.'))
            {
                var propertyName = chain.Substring(0, chain.IndexOf('.'));

                if (Information.CheckPropertyExist(DeclaredType, propertyName))
                {
                    var propertyType = Information.GetPropertyType(DeclaredType, propertyName);
                    var databaseFieldNames = GetDataBaseStorageNames(propertyType, propertyName);
                    
                    var subPath = chain.Substring(chain.IndexOf('.') + 1);
                    foreach (var databaseFieldName in databaseFieldNames)
                    {
                        if (!Joins.ContainsKey(databaseFieldName))
                        {
                            Joins.Add(databaseFieldName, new Chain(propertyType, BasePrefix, Alias, Joins.Count));
                        }
                     
                        Joins[databaseFieldName].Add(subPath);
                    }
                }
                else
                {
                    LogService.LogWarn($"Поле {propertyName} не найдено в классе {DeclaredType.FullName}");
                }
            }
            else
            {
                if (Information.CheckPropertyExist(DeclaredType, chain))
                {
                    var propertyType = Information.GetPropertyType(DeclaredType, chain);
                    var databaseFieldNames = GetDataBaseStorageNames(propertyType, chain);

                    foreach (var databaseFieldName in databaseFieldNames)
                    {
                        if (!LookForUpdateFields.Contains(databaseFieldName)) LookForUpdateFields.Add(databaseFieldName);
                    }
                }
                else
                {
                    LogService.LogWarn($"Поле {chain} не найдено в классе {DeclaredType.FullName}");
                }
            }
        }
        
        public string ReplacePath(List<string> paths, string expression)
        {
            var exp = expression;
            foreach (var path in paths)
            {
                var pathAlias = PathAlias(path);
                exp = !string.IsNullOrEmpty(pathAlias) ? exp.Replace($"@{path}@", pathAlias) : exp;
            }
            
            return exp;
        }

        public string ReplacePath(string path)
        {
            return PathAlias(path);
        }

        public string PathAlias(string path)
        {
            if (path.Contains('.'))
            {
                var propertyName = path.Substring(0, path.IndexOf('.'));
                var subPath = path.Substring(path.IndexOf('.') + 1);
                if (Information.CheckPropertyExist(DeclaredType, propertyName))
                {
                    // пока считаем, что у нас нет полей со множественными typeUsage
                    var databaseFieldName = GetDataBaseStorageNames(Information.GetPropertyType(DeclaredType, propertyName), propertyName)[0];
                    return Joins[databaseFieldName].PathAlias(subPath);
                }
            }

            if (Information.CheckPropertyExist(DeclaredType, path))
            {
                return $"{Alias}.{TGHelper.ChainHelper.Format(GetDataBaseStorageNames(Information.GetPropertyType(DeclaredType, path), path)[0])}";
            }
            else
            {
                LogService.LogWarn($"Поле {path} не найдено в классе {DeclaredType.FullName}");
            }

            return string.Empty;
        }

        /// <summary>
        /// Находит цепочку, которая начинается с таблицы
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Chain FindChain(string table)
        {
            if (TableName == table) return this;

            foreach (var chain in Joins.Values)
            {
                var c = chain.FindChain(table);
                if (c != null) return c;
            }

            return null;
        }

        /// <summary>
        /// Имена таблиц на которые присутствуют в цепочке
        /// </summary>
        public List<string> FromTableNames
        {
            get
            {
                var result = new List<string> { TableName };
                foreach (var join in Joins.Values)
                {
                    result.AddRange(join.FromTableNames);
                }

                return result;
            }
        }

        /// <summary>
        /// Поля, которые обновляются для таблицы
        /// </summary>
        /// <param name="fromTableName"></param>
        /// <returns></returns>
        public List<string> UpdatedFields(string fromTableName)
        {
            var result = new List<string>();
            if (fromTableName == TableName)
            {
                result.AddRange(LookForUpdateFields.Select(x => TGHelper.ChainHelper.Format(x)));
                result.AddRange(Joins.Keys.Select(x => TGHelper.ChainHelper.Format(x)));
            }
            else
            {
                foreach (var join in Joins.Values)
                {
                    result.AddRange(join.UpdatedFields(fromTableName));
                }
            }

            return result.Distinct().ToList();
        }
    }
}