//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpertiseWCFService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Experts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Experts()
        {
            this.ExpertFos = new HashSet<ExpertFos>();
            this.Marks = new HashSet<Marks>();
        }
    
        public int id_expert { get; set; }
        public string surname_expert { get; set; }
        public string name_expert { get; set; }
        public string patronymic_expert { get; set; }
        public string job_expert { get; set; }
        public string post_expert { get; set; }
        public string degree_expert { get; set; }
        public string rank_expert { get; set; }
        public string contacts_expert { get; set; }
        public bool delete_expert { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpertFos> ExpertFos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Marks> Marks { get; set; }
    }
}
