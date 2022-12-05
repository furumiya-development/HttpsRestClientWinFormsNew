using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HttpsRestClientWinForms.Model
{
    /// <summary>JSONデータ格納クラス</summary>
    /// <remarks></remarks>
    public class ShohinEntity
    {
        [JsonPropertyName("uniqueId")]
        public string UniqueId { get; set; }

        [JsonPropertyName("shohinCode")]
        public short ShohinCode { get; set; }

        [JsonPropertyName("shohinName")]
        public string? ShohinName { get; set; }

        [JsonPropertyName("editDate")]
        public decimal EditDate { get; set; }

        [JsonPropertyName("editTime")]
        public decimal EditTime { get; set; }

        [JsonPropertyName("remarks")]
        public string? Remarks { get; set; }
    }
}