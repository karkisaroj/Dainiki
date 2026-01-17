using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Services
{
    public class QuillEditorService
    {
        private readonly IJSRuntime _js;

        public QuillEditorService(IJSRuntime js)
        {
            _js = js;
        }
        public async Task InitializeAsync()
        {
            await Task.Delay(200);
            await _js.InvokeVoidAsync("initializeQuill");
        }
        public async Task<string> GetContentAsync()
        {
            return await _js.InvokeAsync<string>("getQuillContent");
        }
    }
}
