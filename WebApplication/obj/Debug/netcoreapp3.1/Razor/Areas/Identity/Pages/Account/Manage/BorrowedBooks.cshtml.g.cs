#pragma checksum "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\Account\Manage\BorrowedBooks.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "562978ebf2dd959a7d1714b0e7ea1648bdb4c00e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Pages_Account_Manage_BorrowedBooks), @"mvc.1.0.razor-page", @"/Areas/Identity/Pages/Account/Manage/BorrowedBooks.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using Olympia_Library.Areas.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using Olympia_Library.Areas.Identity.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using WebApplication.Repositories;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using Olympia_Library.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\Account\_ViewImports.cshtml"
using Olympia_Library.Areas.Identity.Pages.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\Account\Manage\_ViewImports.cshtml"
using Olympia_Library.Areas.Identity.Pages.Account.Manage;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"562978ebf2dd959a7d1714b0e7ea1648bdb4c00e", @"/Areas/Identity/Pages/Account/Manage/BorrowedBooks.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f3c336034f077cbddd29520d07a896a00cbd9935", @"/Areas/Identity/Pages/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01333bfdbacd265bfefb55c4342e67a3e610019c", @"/Areas/Identity/Pages/Account/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d6989ac5aef464c66b7c078120a31bee886e98ff", @"/Areas/Identity/Pages/Account/Manage/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Identity_Pages_Account_Manage_BorrowedBooks : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationScriptsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n\n\n<script>\n    var doAsyncGet = function () {\n        return $.ajax({\n            url: \'");
#nullable restore
#line 8 "D:\facultate\an4\sem2\Information_Retrieval\WebApp2.0\WebApplication\Areas\Identity\Pages\Account\Manage\BorrowedBooks.cshtml"
             Write(Url.Action("GetBorrowedBooks", "Borrow"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            dataType: ""json"",
        });
    };

    doAsyncGet().done(function (result) {
        console.log(result);
        buildHtmlTable(""#borrowed_books"");

        function buildHtmlTable(selector) {
            var columns = addAllColumnHeaders(result, selector);

            for (var i = 0; i < result.length; i++) {
                var row$ = $('<tr/>');
                for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                    var cellValue = result[i][columns[colIndex]];
                    if (cellValue == null) cellValue = """";
                    row$.append($('<td/>').html(cellValue));
                }
                $(selector).append(row$);
            }
        }

        // Adds a header row to the table and returns the set of columns.
        // Need to do union of keys from all records as some records may not contain
        // all records.
        function addAllColumnHeaders(myList, selector) {
            var columnSet = [];
            var headerTr$ = $(");
            WriteLiteral(@"'<tr/>');

            for (var i = 0; i < myList.length; i++) {
                var rowHash = myList[i];
                for (var key in rowHash) {
                    if ($.inArray(key, columnSet) == -1) {
                        columnSet.push(key);
                        headerTr$.append($('<th/>').html(key));
                    }
                }
            }
            $(selector).append(headerTr$);

            return columnSet;
        }


    });


</script>



<table border=""1"" id=""borrowed_books"">
    
</table>





");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "562978ebf2dd959a7d1714b0e7ea1648bdb4c00e7377", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Areas_Identity_Pages_Account_Manage_BorrowedBooks> Html { get; private set; } = default!;
        #nullable disable
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Areas_Identity_Pages_Account_Manage_BorrowedBooks> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Areas_Identity_Pages_Account_Manage_BorrowedBooks>)PageContext?.ViewData;
        public Areas_Identity_Pages_Account_Manage_BorrowedBooks Model => ViewData.Model;
    }
}
#pragma warning restore 1591
