namespace OrderServices.Models
{
    public class ModelHelper
    {

        public static void CopySalesOrderProperty(TSalesOrder source, ref TSalesOrder target)
        {
            var sProp = source.GetType().GetProperties().ToList();
            foreach (var tProp in target.GetType().GetProperties())
            {
                var eqProp = sProp.Find(m => m.Name == tProp.Name & m.PropertyType == tProp.PropertyType);
                if (eqProp != null & tProp.CanWrite)
                    tProp.SetValue(target, eqProp.GetValue(source));
            }
        }

        public static void CopyCancelOrderProperty(TCancelOrder source, ref TCancelOrder target)
        {
            var sProp = source.GetType().GetProperties().ToList();
            foreach (var tProp in target.GetType().GetProperties())
            {
                var eqProp = sProp.Find(m => m.Name == tProp.Name & m.PropertyType == tProp.PropertyType);
                if (eqProp != null & tProp.CanWrite)
                    tProp.SetValue(target, eqProp.GetValue(source));
            }
        }

        public static void CopyDeliveryOrderProperty(TDeliveryOrder source, ref TDeliveryOrder target)
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
