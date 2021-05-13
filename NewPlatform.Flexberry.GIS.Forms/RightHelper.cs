namespace NewPlatform.Flexberry.GIS.Forms
{
    using System;
    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Security;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using Unity;

    public static class RightHelper
    {
        public static void ApplyRightsOnWolv(WebObjectListView wolv, Type type)
        {
            ISecurityManager securityManager = UnityFactory.GetContainer().Resolve<ISecurityManager>();

            if (!securityManager.AccessObjectCheck(type, tTypeAccess.Update, false))
            {
                wolv.Operations.EditInRow = false;
                wolv.Operations.Edit = false;
            }

            if (!securityManager.AccessObjectCheck(type, tTypeAccess.Insert, false))
            {
                wolv.Operations.New = false;
                wolv.Operations.NewByExampleInRow = false;
            }

            if (!securityManager.AccessObjectCheck(type, tTypeAccess.Delete, false))
            {
                wolv.Operations.DeleteInRow = false;
                wolv.Operations.Delete = false;
            }
        }

        public static bool CheckReadOnlyForm(Type type, ObjectStatus status)
        {
            ISecurityManager securityManager = UnityFactory.GetContainer().Resolve<ISecurityManager>();

            if (!securityManager.AccessObjectCheck(type, tTypeAccess.Insert, false) &&
                status == ObjectStatus.Created) return true;

            if (!securityManager.AccessObjectCheck(type, tTypeAccess.Update, false) &&
                status != ObjectStatus.Created) return true;

            return false;
        }
    }
}
