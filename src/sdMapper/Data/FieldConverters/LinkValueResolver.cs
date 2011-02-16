using System;
using System.Xml.Linq;
using Lemon.Domain;
using Lemon.Extensions;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
    public class LinkValueResolver : IValueResolver
    {
        #region IValueResolver Members

        public bool CanResolve(Type type)
        {
            return type == typeof(Link);
        }

        public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            if (String.IsNullOrEmpty(rawValue))
            {
                return null;
            }

            try
            {
                if (rawValue.IsGuid())
                {
                    Guid linkId = new Guid(rawValue);
                    Item item = Context.Database.GetItem(new ID(linkId));
                    return new Link { Id = linkId, LinkType = LinkTypes.Internal, Caption = item.Name, Url = item.GetItemUrl() };
                }
                else
                {
                    XDocument xmlField = XDocument.Parse(rawValue);
                    XElement linkElement = xmlField.Element("link");
                    return LinkUrl.GetUrl(linkElement, Context.Database);
                }
            }
            catch (Exception)
            {
                // not xml or guid
                return null;
            }
        }

        public object ResolveItemFieldValue(object rawValue)
        {

            Link link = rawValue as Link;

            if (link == null) return String.Empty;

            if (link.LinkType == LinkTypes.Internal)
            {
                var item = Context.Database.GetItem(new ID(link.Id));

                string fieldValue =
                    String.Format(
                        "<link linktype=\"internal\" url=\"/{0}\" querystring=\"\" id=\"{1}\" target=\"\" />",
                        item.Paths.ContentPath, link.Id.ToString("B").ToUpper());

                return fieldValue;

            }
            else
            {
                throw new NotImplementedException("Cannot convert link type to sitecore xml field. YET");
            }
        }

        #endregion
    }
}
