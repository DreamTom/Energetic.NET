
namespace Energetic.NET.Basic.Application.ResourceService.Dto
{
    /// <summary>
    /// 菜单下拉树
    /// </summary>
    public record SimpleResourceTreeResponse(long Id, string Name, long ParentId)
    {
        public string Title => Name;

        public bool Spread { get; set; } = true;

        public string Label => Name;

        public string Value => Id.ToString();

        public string? Icon { get; set; }

        public List<SimpleResourceTreeResponse> Children { get; set; } = [];
    }
}
