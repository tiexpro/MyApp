using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using MyApp.Models;
using System.Collections.Generic;

namespace MyApp.Extensions
{
    public static class MenuExtension
    {
        /// <summary>
        /// Menu extension for HTML Helper
        /// </summary>
        /// <param name="htmlHelper"> Base  html Helpers</param>
        public static IHtmlContent Menu(this IHtmlHelper htmlHelper)
        {

            var Fac = (IUrlHelperFactory)htmlHelper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var Url = Fac.GetUrlHelper(htmlHelper.ViewContext);

            var menuItems = new List<Menu>();

            menuItems.Add(new Menu {
                id = 1,
                child = false,
                icon = "speedometer",
                name = "Dashboard",
                area = "Dashboard",
                controller = "Dashboard",
                action = "Index"
            });

            menuItems.Add(new Menu
            {
                id = 2,
                type = 0,
                child = true,
                icon = "folder-open",
                name = "Submenu",
            });

            menuItems.Add(new Menu
            {
                id = 3,
                parent = 2,
                child = false,
                icon = "radio-button-off",
                name = "Link to page A",
                area = "PageA",
                controller = "PageA",
                action = "Index"
            });

            return Build(menuItems, Url);
            
        }


        /// <summary>
        /// Build the entire list
        /// </summary>
        /// <param name="Items"> List with menu items</param>
        /// <param name="Url"> UrlHelper of ViewContext</param>
        private static IHtmlContent Build(List<Menu> Items, IUrlHelper Url)
        {
            var doc = new HtmlDocument();

            foreach (var item in Items)
            {
                var rut = Url.Action(item.action, item.controller, new { area = item.area });
                var act = Url.ActionContext.HttpContext.Request.Path;
                var parent = doc.DocumentNode.SelectSingleNode($"//ul[@id='menu-item-{item.parent}']");

                if (parent == null)
                {
                    doc.DocumentNode.AppendChild(add(item, rut, act));
                }
                else
                {
                    var li = add(item, rut, act);

                    if (li.FirstChild.HasClass("active"))
                    {
                        parent.ParentNode.AddClass("menu-open");
                        parent.PreviousSibling.AddClass("active");
                    }

                    parent.AppendChild(li);
                }
            }

            return new HtmlString(doc.DocumentNode.OuterHtml);
        }

        /// <summary>
        /// Build each node
        /// </summary>
        private static HtmlNode add(Menu item, string url, string act)
        {
            var p = HtmlNode.CreateNode($"<p>{item.name}</p>");
            var li = HtmlNode.CreateNode("<li></li>");
            var a = HtmlNode.CreateNode("<a></a>");

            li.AddClass("nav-item");
            a.AddClass("nav-link");
            a.SetAttributeValue("href", item.type == 0 ? "#" : url);

            if (act.StartsWith(url) && item.type != 0 && url.Length > 1)
            {
                a.AddClass("active");
            }

            if (url.Length == 1 && item.type != 0 && act == @"/")
            {
                a.AddClass("active");
            }

            var i = HtmlNode.CreateNode("<i></i>");
            i.AddClass($"nav-icon ion-2x icon ion-md-{item.icon}");

            if (item.child)
            {
                li.AddClass("has-treeview");
            }

            a.AppendChild(i);
            a.AppendChild(p);
            li.AppendChild(a);

            if (item.child)
            {
                var i2 = HtmlNode.CreateNode("<i></i>");
                i2.AddClass("right ion ion-ios-arrow-back");
                p.AppendChild(i2);

                var ul = HtmlNode.CreateNode("<ul></ul>");
                ul.AddClass("nav nav-treeview");
                ul.Id = $"menu-item-{item.id}";
                li.AppendChild(ul);
            }

            return li;
        }        
    }
}
