using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    public class MapExpressionField : MapField
    {
        /// <summary>
        /// </summary>
        public string LayerField { get; private set; }
        
        /// <summary>
        /// </summary>
        public string Expression { get; private set; }

        /// <summary>
        /// </summary>
        public List<string> Paths { get; private set; }
        
        /// <summary>
        /// </summary>
        public MapExpressionField(Type type, string field, string sqlExpression) : base(type)
        {
            LayerField = field;
            Expression = sqlExpression;
            Paths = new List<string>();

            var fields = new List<string>();
            var match = Regex.Match(Expression, @"@\b(\w+\.{0,1})+\b@");
            while (match.Success)
            {
                if (!fields.Contains(match.Value))
                {
                    fields.Add(match.Value);
                }

                match = match.NextMatch();
            }

            fields.Sort((a, b) => b.Length.CompareTo(a.Length));
            foreach (var path in fields)
            {
                Chain.Add(path.Trim('@'));
                Paths.Add(path.Trim('@'));
            }
        }
    }
}
