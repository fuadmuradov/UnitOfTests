using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitOfTestProject.Services;

namespace UnitOfTestProject
{
    public class ApplicationEvaluation
    {
        public ApplicationEvaluation(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        public const int MinAge = 18;
        private readonly IIdentityService identityService;
        public List<string> RequiredSkills = new List<string> { "C#", "Aspnet Core", "Microservice", "RabbitMQ" };
        public ApplicantSituation Evaluator(JobApplication form)
        {

            if (form.Applicant.Age < MinAge)
                return ApplicantSituation.AutoRejected;
            int eng = EnoughRequired(form.skillList);
            if (eng < 50)
                return ApplicantSituation.AutoRejected;

            if (EnoughRequired(form.skillList) < 75 && form.YearsOfExpperiance < 10)
                return ApplicantSituation.AutoRejected;

            if (identityService.IsValid(form.Applicant.IdentityNumber))
                return ApplicantSituation.AutoAccepted;

            if (!identityService.IsValid(form.Applicant.IdentityNumber))
                return ApplicantSituation.TransferedToHR;

           

           return ApplicantSituation.AutoAccepted;
        }

        public int EnoughRequired(List<string> skills)
        {
            var skillCount =
                skills.Where(x => RequiredSkills.Contains(x, StringComparer.OrdinalIgnoreCase)).Count();
            int percent = (int)(((double)skillCount / RequiredSkills.Count) * 100);
            return percent;
                
        }


        public enum ApplicantSituation
        {
            AutoAccepted,
            AutoRejected,
            TransferedToHR,
            TransferedToTeamLead,
            TransferedToCTO
        }
    }
}
