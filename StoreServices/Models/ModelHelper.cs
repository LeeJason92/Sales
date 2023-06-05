namespace StoreServices.Models
{
    public class ModelHelper
    {

        public static void CopyProperty(MStore source, ref MStore target)
        {
            var sProp = source.GetType().GetProperties().ToList();
            foreach (var tProp in target.GetType().GetProperties())
            {
                var eqProp = sProp.Find(m => m.Name == tProp.Name & m.PropertyType == tProp.PropertyType);
                if (eqProp != null & tProp.CanWrite)
                    tProp.SetValue(target, eqProp.GetValue(source));
            }
        }

        public static void CopyMarketingAreaProperty(MMarketingArea source, ref MMarketingArea target)
        {
            var sProp = source.GetType().GetProperties().ToList();
            foreach (var tProp in target.GetType().GetProperties())
            {
                var eqProp = sProp.Find(m => m.Name == tProp.Name & m.PropertyType == tProp.PropertyType);
                if (eqProp != null & tProp.CanWrite)
                    tProp.SetValue(target, eqProp.GetValue(source));
            }
        }
    }
}
