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
    
    public partial class ProjectExpertise
    {
        public int id_project_expertise { get; set; }
        public int id_expertise { get; set; }
        public int id_project { get; set; }
        public bool accept { get; set; }
    
        public virtual Expertises Expertises { get; set; }
    }
}
