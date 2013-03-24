using System.Web.NHaml.TemplateResolution;

namespace NHaml.Tests.Builders
{
    public static class ViewSourceBuilder
    {
        public static ViewSource Create()
        {
            return Create("Test");
        }

        public static ViewSource Create(string content, string fileName = @"c:\test.haml")
        {
            var stubViewSource = new StreamViewSource(content, fileName);
            return stubViewSource;
        }
    }
}
