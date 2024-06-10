using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ColorNote_Backup_Viewer.Model
{
    public class HTMLDocumentManager
    {
        public void SaveAs(MemoData data, string filePath)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.DocumentNode.AppendChild(HtmlNode.CreateNode("<!DOCTYPE html>"));

            HtmlNode root = doc.DocumentNode.AppendChild(doc.CreateElement("html"));

            HtmlNode head = root.AppendChild(doc.CreateElement("head"));
            HtmlNode title = head.AppendChild(doc.CreateElement("title"));
            title.AppendChild(HtmlNode.CreateNode(data.title));

            HtmlNode body = root.AppendChild(doc.CreateElement("body"));
            body.AppendChild(doc.CreateElement("h1")).AppendChild(HtmlNode.CreateNode(data.title));
            HtmlNode textDiv = body.AppendChild(doc.CreateElement("div"));
            textDiv.Attributes.Add("style", "border:2px solid black; padding:10px; white-space:pre; display:inline-block");
            textDiv.AppendChild(HtmlNode.CreateNode(data.text));

            doc.Save(filePath);
        }
    }
}
