#pragma checksum "C:\Projects\Mobile\b2b.project\b2b.project\viva.admin\Views\GrupeArtikalas\_Pod_grupe_table.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "e6b8bd113d3a28d85d94f0ee768815de81008329e1bcff9ab486bba0b3f77ce8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_GrupeArtikalas__Pod_grupe_table), @"mvc.1.0.view", @"/Views/GrupeArtikalas/_Pod_grupe_table.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Projects\Mobile\b2b.project\b2b.project\viva.admin\Views\_ViewImports.cshtml"
using viva.admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Projects\Mobile\b2b.project\b2b.project\viva.admin\Views\_ViewImports.cshtml"
using viva.admin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"e6b8bd113d3a28d85d94f0ee768815de81008329e1bcff9ab486bba0b3f77ce8", @"/Views/GrupeArtikalas/_Pod_grupe_table.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"0ac74086b4efe22ead0872da30bf317bd0f63e1e36f374c3d44c0981da35e227", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_GrupeArtikalas__Pod_grupe_table : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<viva.admin.Models.Grupe.Pod_Grupe_VM>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div>
    <h3 id=""label_podgrupa_grupa""></h3>
    <div>
        <input id=""hidden_grupa_id"" type=""hidden"" />
        <input id=""input_add_pod_grupa"" type=""text""/> 
        <button id=""btn_add_pod_grupa"" class=""btn btn-info"">Dodaj</button>
    </div>
    <div>

    </div>
    <table id=""tbl_podgrupe"" class=""table table-striped"">
        <thead>
            <tr><td>Naziv</td></tr>
        </thead>
        <tbody>
");
#nullable restore
#line 22 "C:\Projects\Mobile\b2b.project\b2b.project\viva.admin\Views\GrupeArtikalas\_Pod_grupe_table.cshtml"
             foreach(var record in Model.Pod_Grupe)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr><td>");
#nullable restore
#line 24 "C:\Projects\Mobile\b2b.project\b2b.project\viva.admin\Views\GrupeArtikalas\_Pod_grupe_table.cshtml"
                   Write(record.Naziv);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td></tr>\r\n");
#nullable restore
#line 25 "C:\Projects\Mobile\b2b.project\b2b.project\viva.admin\Views\GrupeArtikalas\_Pod_grupe_table.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<viva.admin.Models.Grupe.Pod_Grupe_VM> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
