using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class PostageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? TextBody { get; set; }
        public string? PathToPhoto { get; set; }
        public DateTime CreationDate { get; set; }

        // Chave estrangeira para o post "pai", pode ser anulavel para permitir a criação de postagens originais
        public int? ParentPostageId { get; set; }
        public PostageModel? ParentPostage { get; set; }
        public ICollection<PostageModel> Replies { get; set; } = new List<PostageModel>();

        public int StudentStudiesGroupStudiesGroupId { get; set; }
        public int StudentStudiesGroupStudentId { get; set; }
        public StudentStudiesGroupModel StudentStudiesGroup { get; set; }
    }
}