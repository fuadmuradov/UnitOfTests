using NUnit.Framework;
using UnitOfTestProject;
using Moq;
using static UnitOfTestProject.ApplicationEvaluation;
using UnitOfTestProject.Services;

namespace JobApplicationUnitTest
{
    public class ApplicationEvaluationUnitTest
    {
   

        [Test]
        public void Applicant_WithUnderAge_TrasferedToAutoReject()
        {
            //Arrane
            var evaluate = new ApplicationEvaluation(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 17
                }
            };

            //Action
            var appResult = evaluate.Evaluator(form);
            //Assert
            Assert.AreEqual(appResult, ApplicantSituation.AutoRejected);
        }

        [Test]
        public void Applicant_RequiredUnderEnough_TrasferedToAutoReject()
        {
            //Arrane
            var evaluate = new ApplicationEvaluation(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 20
                },
                skillList = new System.Collections.Generic.List<string>() {"" }
                
            };

            //Action
            var appResult = evaluate.Evaluator(form);
            //Assert
            Assert.AreEqual(appResult, ApplicantSituation.AutoRejected);
        }

        [Test]
        public void Application_RequiredSkill_and_RequiredExperienceYear_UnderEnough_TransferedtoAutoReject()
        {
            //Arrange
            var evaluate = new ApplicationEvaluation(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 20,
                    IdentityNumber ="12345"
                },
                skillList = new System.Collections.Generic.List<string>() { "C#", "Aspnet Core", "Microservice" },
                YearsOfExpperiance = 9
            };
            //Action
            var appResult = evaluate.Evaluator(form);
            //Assert
            Assert.AreEqual(appResult, ApplicantSituation.AutoRejected);
        }

        [Test]
        public void Applicant_IdentityNumberIsNotValid_TransferToHR()
        {
            var mockValidator = new Mock<IIdentityService>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
            var evaluate = new ApplicationEvaluation(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 20
            },
                skillList = new System.Collections.Generic.List<string>() { "C#", "Aspnet Core", "Microservice" },
                YearsOfExpperiance = 20
            };

            var appResult = evaluate.Evaluator(form);

            Assert.AreEqual(appResult, ApplicantSituation.TransferedToHR);

        }

        [Test]
        public void Applicant_IdentityNumberIValid_AutoAccepted()
        {
            var mockValidator = new Mock<IIdentityService>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            var evaluate = new ApplicationEvaluation(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 20
                },

                 skillList = new System.Collections.Generic.List<string>() { "C#", "Aspnet Core", "Microservice" },
                YearsOfExpperiance = 20
            };
        
            var appResult = evaluate.Evaluator(form);

            Assert.AreEqual(appResult, ApplicantSituation.AutoAccepted);


        }
    }
}