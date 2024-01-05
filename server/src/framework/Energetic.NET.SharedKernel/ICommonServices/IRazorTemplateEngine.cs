using RazorEngineCore;

namespace Energetic.NET.SharedKernel.ICommonServices
{
    public interface IRazorTemplateEngine
    {
        /// <summary>
        /// 渲染razor模板
        /// </summary>
        /// <typeparam name="T">传入模板类型</typeparam>
        /// <param name="templatePath">模板路径</param>
        /// <param name="model">传入模板对象</param>
        /// <returns></returns>
        Task<string> RenderAsync<T>(string templatePath, T model) where T: RazorEngineTemplateBase;

        /// <summary>
        /// 渲染razor模板
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="model">传入模板对象</param>
        /// <returns></returns>
        Task<string> RenderAsync(string templatePath, object model);
    }
}
