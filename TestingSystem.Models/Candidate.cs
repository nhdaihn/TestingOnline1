using System.Collections;
using System.Collections.Generic;

namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Candidate
    {
	    [Key]
	    [ForeignKey("Users")]
	    public int CandidateID { get; set; }

	    public virtual User Users { get; set; }
	    public ICollection<TestResult> TestResults { get; set; }
	}
}
