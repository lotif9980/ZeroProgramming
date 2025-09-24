using System.ComponentModel.DataAnnotations;

namespace ZPWEB.Enum
{

    public enum DomainEnum
    {
        Live = 1,
        Test = 2
    }

    public enum TeamType
    {
        Engineer=1,
        SQA=2,

        [Display(Name = "Support Engineer")]
        SupportEngineer =3
                    
    }

    public enum IssueStatus
    {
        pending=1,
        solved=2
    }

    public enum CustomerIssueStatus
    {
        pending=1,
        solved=2
    }

    public enum CustomerSolvedIssueStatus
    {
        Solved = 1,
        Delete =2
    }

    public enum OurCustomerStatus
    {
        Active=1,
        InActive=2
    }
}
