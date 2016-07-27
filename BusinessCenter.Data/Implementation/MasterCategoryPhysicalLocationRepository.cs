using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class MasterCategoryPhysicalLocationRepository : GenericRepository<MasterCategoryPhysicalLocation>, IMasterCategoryPhysicalLocationRepository
    {
        protected IMasterPrimaryCategoryRepository primaryrepo;
        protected IMasterCategoryQuestionRepository catquestionrepo;
        protected IOSubCategoryFeesRepository subCategoryrepo;
        protected IMasterSecondaryLicenseCategoryRepository secondaryrepo;
        public MasterCategoryPhysicalLocationRepository(IUnitOfWork context, IMasterPrimaryCategoryRepository primaryRepository,
            IMasterCategoryQuestionRepository catquestionrepository,
            IOSubCategoryFeesRepository subCategoryRepository, 
            IMasterSecondaryLicenseCategoryRepository secondaryRepository)
            : base(context)
        {
            primaryrepo = primaryRepository;
            catquestionrepo = catquestionrepository;
            subCategoryrepo = subCategoryRepository;
            secondaryrepo = secondaryRepository;
        }
        /// <summary>
        /// This method is used to display all Physical Location for Anuglar and admin as well.
        /// </summary>
        /// <returns>Primary Category list</returns>
        public IEnumerable<MasterCategoryPhysicalLocation> AllCategoryPhysicallocations()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to get the specific Physical Location based on Primary Id.
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns>Specific primary category Data</returns>
        public IEnumerable<MasterCategoryPhysicalLocation> FindByID(SubmissionApplication submissionApplication)
        {
            var physicallocation = FindBy(x => x.PrimaryCategory_Id == submissionApplication.PrimaryID );
            return physicallocation;
        }
        /// <summary>
        /// This method is used to get the specific Physical Location based on Category Id.
        /// </summary>
        /// <param name="Categoryid"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryPhysicalLocation> FindCategoryID(string Categoryid)
        {
            var physicallocation = FindBy(x => x.PrimaryCategory_Id == Categoryid);
            return physicallocation;
        }
        /// <summary>
        /// This method is used to retrive Required Questions for the application creation based on Submission Application.
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns>list of question for the Application</returns>
        public IEnumerable<SubmissionApplication> AllScreeningQuestions(SubmissionApplication submissionApplication)
        {
            try
            {
                var questions = new List<SubmissionApplication>();
                var screeningQuestions = new List<ScreeningQuestion>();
                int startRange = 1;
                var primarycategory = primaryrepo.FindByprimaryID(submissionApplication).FirstOrDefault();
                int endRange = 99999;
                var primarystart = subCategoryrepo.FindByDescription(primarycategory.Description.Replace(System.Environment.NewLine, string.Empty)).OrderBy(x=>x.Start).ToList();
                if (primarystart.Count() != 0)
                {
                    var primary = primarystart.Where(x => x.Start == 0).ToList();
                    if (primary.Count != 0)
                    { startRange = 1; }
                    else
                    { startRange = primarystart.FirstOrDefault().Start == null ? 1 : primarystart.FirstOrDefault().Start == 0 ? 1 : Convert.ToInt32(primarystart.FirstOrDefault().Start); }
                    endRange = primarystart.LastOrDefault().End == null? 99999: primarystart.LastOrDefault().End == 0? 99999
                            : Convert.ToInt32(primarystart.LastOrDefault().End);
                }
                ScreeningQuestion screenquestion2 = new ScreeningQuestion();
                if (primarycategory.App_Type.ToUpper() != "I")
                {

                    ScreeningQuestion isregistered = new ScreeningQuestion();
                    isregistered.Question =
                        GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsDcraRegisteredInCorporation);
                    isregistered.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.RadioButton);
                    isregistered.Option = new List<BusinessStructure>()
                    {
                        new BusinessStructure()
                        {
                            BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO)
                        }
                    };
                    isregistered.Answer = "";
                    screenquestion2.keyIdentifying = "";
                    screeningQuestions.Add(isregistered);

                    ScreeningQuestion screenstructure = new ScreeningQuestion();
                    screenstructure.Question =
                        GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.BusinessStructure);
                    screenstructure.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Dropdown);
                    screenstructure.Option = new List<BusinessStructure>()
                    {
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.Select)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.CorpProfit)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.CorpNonProfit)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.Llc)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.Lp)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.Llp)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.Gca)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.Lca)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.StatutoryTrust)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.SoleProprietorship)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.GeneralPartnership)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption =
                                GenericEnums.GetEnumDescription(GenericEnums.BusinessStructureOptions.JointVenture)
                        }
                    };
                    screenstructure.Answer = "";
                    screenquestion2.keyIdentifying = "";
                    screeningQuestions.Add(screenstructure);
                    ScreeningQuestion tradename = new ScreeningQuestion();
                    tradename.Question = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.TradeName);
                    tradename.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Textbox);
                    tradename.Option = new List<BusinessStructure>()
                    {
                        new BusinessStructure()
                        {
                            BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO)
                        }
                    };
                    tradename.Answer = "";
                    screenquestion2.keyIdentifying = "";
                    screeningQuestions.Add(tradename);
                }
                string Unitone;
                string Unittwo;
                Unitone =primarycategory.UnitOne ==null ? "": (primarycategory.UnitOne ?? "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NA) ? "" : primarycategory.UnitOne.Replace(System.Environment.NewLine, string.Empty).Trim();
                Unittwo = primarycategory.UnitTwo == null ? "" : (primarycategory.UnitTwo ?? "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NA) ? "" : primarycategory.UnitTwo.Replace(System.Environment.NewLine, string.Empty).Trim();
                if (Unitone != "")
                {
                    var unitonequestion = catquestionrepo.FindByID(primarycategory.Description.Replace(System.Environment.NewLine, ""), Unitone).ToList();
                    if (unitonequestion.Count != 0)
                    {
                        foreach (var unitquestion in unitonequestion)
                        {
                            ScreeningQuestion screenprimary = new ScreeningQuestion();
                            screenprimary.Question = unitquestion.UserQuestion.Replace(System.Environment.NewLine, "");
                            screenprimary.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Textbox);
                            screenprimary.Option = null;
                            screenprimary.Answer = "";
                            screenprimary.CategoryId = primarycategory.PrimaryID;
                            screenprimary.QuestionFor = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Primary);
                            screenprimary.StartRange = startRange;
                            screenprimary.EndRange = endRange;
                            screenprimary.EndRange = endRange;
                            screeningQuestions.Add(screenprimary);
                        }
                    }
                }
                if (Unittwo != "")
                {
                    var unittwoquestion = catquestionrepo.FindByID(primarycategory.Description.Replace(System.Environment.NewLine, ""), Unittwo).ToList();
                    if (unittwoquestion.Count != 0)
                    {
                        foreach (var unitquestion in unittwoquestion)
                        {
                            ScreeningQuestion screenprimary1 = new ScreeningQuestion();
                            screenprimary1.Question = unitquestion.UserQuestion.Replace(System.Environment.NewLine, "");
                            screenprimary1.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Textbox);
                            screenprimary1.Option = null;
                            screenprimary1.Answer = "";
                            screenprimary1.CategoryId = primarycategory.PrimaryID;
                            screenprimary1.QuestionFor = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Primary);
                            screenprimary1.StartRange = startRange;
                            screenprimary1.EndRange = endRange;
                            screeningQuestions.Add(screenprimary1);
                        }
                    }
                }
                if (submissionApplication.Secondary != null)
                {
                    var secondary = submissionApplication.Secondary.Split(',');
                    if (secondary != null)
                    {
                        foreach (var secondaryid in secondary)
                        {
                            if (secondaryid.Trim() != ",")
                            {
                                var secondaryCategory = secondaryrepo.FindBySecondaryID(secondaryid);
                                foreach (var item in secondaryCategory)
                                {
                                    var secondaryStart = subCategoryrepo.FindByDescription(item.SecondaryLicenseCategory.Replace(System.Environment.NewLine, string.Empty).Trim()).OrderBy(x=>x.Start).ToList();
                                    if (secondaryStart.Count != 0)
                                    {
                                        var secondarycategory = secondaryStart.Where(x => x.Start == 0).ToList();
                                        if (secondarycategory.Count() != 0)
                                        { startRange = 1; }
                                        else
                                        { startRange = secondaryStart.FirstOrDefault().Start == null ? 1 : secondaryStart.FirstOrDefault().Start == 0 ? 1 : Convert.ToInt32(secondaryStart.FirstOrDefault().Start); }
                                        endRange = secondaryStart.LastOrDefault().End == null ? 99999 : secondaryStart.LastOrDefault().End == 0 ? 99999
                                            : Convert.ToInt32(secondaryStart.LastOrDefault().End);
                                    }
                                    var userQuestions = catquestionrepo.FindBySecondaryName(item.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim()).ToList();
                                    foreach (var question in userQuestions)
                                    {
                                        ScreeningQuestion userSecondaryQuestion = new ScreeningQuestion();
                                        userSecondaryQuestion.Question = question.UserQuestion.Replace(System.Environment.NewLine, "");
                                        userSecondaryQuestion.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Textbox);
                                        userSecondaryQuestion.Option = null;
                                        userSecondaryQuestion.Answer = "";
                                        userSecondaryQuestion.CategoryId = item.SecondaryID;
                                        userSecondaryQuestion.QuestionFor = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Secondary);
                                        userSecondaryQuestion.StartRange = startRange;
                                        userSecondaryQuestion.EndRange = endRange;
                                        //userSecondaryQuestion.EndRange = endRange;
                                        screeningQuestions.Add(userSecondaryQuestion);
                                    }
                                }
                            }
                        }
                    }
                }
                    //if (primarycategory.App_Type.ToUpper() != "I")
                    //{
                       // ScreeningQuestion businessOwner = new ScreeningQuestion();
                       // businessOwner.Question =
                       //     GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.BusinessOwner);
                       // businessOwner.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Textbox);
                       // businessOwner.Option = null;
                       // businessOwner.Answer = "";
                       // businessOwner.keyIdentifying = "";
                       //screeningQuestions.Add(businessOwner);
                    //}
                    //else
                    //{
                    //    ScreeningQuestion businessOwner = new ScreeningQuestion();
                    //    businessOwner.Question =
                    //        GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IndividualBusinessOwner);
                    //    businessOwner.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Textbox);
                    //    businessOwner.Option = null;
                    //    businessOwner.Answer = "";
                    //    businessOwner.keyIdentifying = "";
                    //    screeningQuestions.Add(businessOwner);
                    //}

                    screenquestion2.Question = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.DurationOfLicense);
                screenquestion2.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.RadioButton);
                screenquestion2.Option = new List<BusinessStructure>()
                                    {
                                     new BusinessStructure(){BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.TwoYear)},
                                    new BusinessStructure(){BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.FourYear)}
                                    };
                screenquestion2.Answer = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.TwoYear);
                screenquestion2.keyIdentifying = "";
                screeningQuestions.Add(screenquestion2);
                var Cofoquestion = FindByID(submissionApplication).FirstOrDefault();
                string BusinessinDC = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO);
                string IsHOP = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO);
                int hopcount = 0;
                int categorycount = 0;
                var businessmustbe = FindByID(submissionApplication).ToList();
                if (businessmustbe.Count() != 0)
                {
                    categorycount = categorycount + 1;
                    BusinessinDC = businessmustbe.FirstOrDefault().BusinessMustBeInDC.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                    IsHOP = businessmustbe.FirstOrDefault().HOP_EHOPAllowed.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                    if (IsHOP.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES))
                    {
                        hopcount = hopcount + 1;
                    }
                }

                if (submissionApplication.Secondary != null)
                {
                    var Secondarycat = submissionApplication.Secondary.Split(',');
                    foreach (var secondary in Secondarycat)
                    {
                        if (secondary.Trim() != ",")
                        {
                            categorycount = categorycount + 1;
                            var secondaryname = secondaryrepo.FindBySecondaryID(secondary).ToList();
                            if (secondaryname.Count() != 0)
                            {
                                string primaryname = secondaryname.First().SecondaryLicenseCategory.Replace(System.Environment.NewLine, "") ?? "";
                                if (primaryname != "")
                                {
                                    var primaryid = primaryrepo.SecondaryEndorsement(primaryname).ToList();
                                    if (primaryid.Count() != 0)
                                    {
                                        string idprimary = primaryid.First().PrimaryID.Replace(System.Environment.NewLine, "") ?? "";
                                        var mustindc = FindCategoryID(idprimary).ToList();
                                        if (mustindc.Count() != 0)
                                        {
                                            if (mustindc.First().BusinessMustBeInDC.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES))
                                            {
                                                BusinessinDC = mustindc.First().BusinessMustBeInDC.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                                            }
                                            if (mustindc.First().HOP_EHOPAllowed.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES))
                                            {
                                                IsHOP = mustindc.First().HOP_EHOPAllowed.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                                                hopcount = hopcount + 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (businessmustbe != null)
                {
                    if (BusinessinDC != GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES))
                    {
                        ScreeningQuestion indc = new ScreeningQuestion();
                        indc.Question = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.LocationMustBeInDc);
                        indc.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.RadioButton);
                        indc.Option = new List<BusinessStructure>()
                                     {
                                         new BusinessStructure(){BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES)},
                                    new BusinessStructure(){BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO)}
                                     };
                        indc.Answer = "";
                        indc.keyIdentifying = BusinessinDC;
                        screeningQuestions.Add(indc);
                    }
                }
                //if (primarycategory.App_Type.Replace(System.Environment.NewLine, "").ToUpper().Trim() !=
                //    GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.I) ||
                //    primarycategory.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                //    GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.SOLICITOR))
                //{
                    if (IsHOP == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES) &&
                        categorycount == hopcount)
                    {
                        if (Cofoquestion != null)
                        {
                            ScreeningQuestion screencof = new ScreeningQuestion();
                            screencof.Question =
                                GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsHomeBased);
                            screencof.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.RadioButton);
                            screencof.Option = new List<BusinessStructure>()
                            {
                                new BusinessStructure()
                                {
                                    BusinessStructureOption =
                                        GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES)
                                },
                                new BusinessStructure()
                                {
                                    BusinessStructureOption =
                                        GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO)
                                }
                            };
                            screencof.Answer = "";
                            screeningQuestions.Add(screencof);
                            ScreeningQuestion screenhof = new ScreeningQuestion();
                            screenhof.Question =
                                GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsEhopAllowed);
                            screenhof.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.RadioButton);
                            screenhof.Option = new List<BusinessStructure>()
                            {
                                new BusinessStructure()
                                {
                                    BusinessStructureOption =
                                        GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES)
                                },
                                new BusinessStructure()
                                {
                                    BusinessStructureOption =
                                        GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO)
                                }
                            };
                            screenhof.Answer = "";
                            screenquestion2.keyIdentifying = "";
                            screeningQuestions.Add(screenhof);
                        }
                    }
                //}
                if (primarycategory.App_Type.ToUpper() != "I")
                {

                ScreeningQuestion screenssn = new ScreeningQuestion();
                    screenssn.Question =
                        GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.TaxAndRevenueValidate);
                    screenssn.Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.RadioButton);
                    screenssn.Option = new List<BusinessStructure>()
                    {
                        new BusinessStructure()
                        {
                            BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.FEIN)
                        },
                        new BusinessStructure()
                        {
                            BusinessStructureOption = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.SSN)
                        }
                    };
                    screenssn.Answer = "";
                    screenquestion2.keyIdentifying = "";
                    screeningQuestions.Add(screenssn);
                }
                var subapp = new SubmissionApplication
                {
                    App_Type = primarycategory.App_Type.Replace(System.Environment.NewLine, "").ToUpper().Trim(),
                    SubQuestion = screeningQuestions
                };
                questions.Add(subapp);
                return questions;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to create or edit Physical Location through Admin portal.
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns>Retrun result in Numbers</returns>
        public string InsertUpdatePhysicalLocation(PrimaryPhysicallocation primaryPhysicallocation)
        {
            string result = string.Empty;
            var validate = FindBy(x => x.PrimaryCategory_Id == primaryPhysicallocation.PrimaryID);
            var categoryphysicallocation = new MasterCategoryPhysicalLocation
            {
                PrimaryCategory_Id = primaryPhysicallocation.PrimaryID,
                BusinessMustBeInDC = primaryPhysicallocation.BusinessMustbeinDC,
                COFORequired = primaryPhysicallocation.CofORequired,
                HOP_EHOPAllowed = primaryPhysicallocation.HOP_EHOPAllowed,
                ExemptFromAllFees = primaryPhysicallocation.ExemptfromAllFees,
                LicenseType = primaryPhysicallocation.LicenseType,
                Status = true
            };
            if (!validate.Any())
            {
                Add(categoryphysicallocation);
                Save();
                result = "1";
            }
            else
            {
                var updatephysicalLocation = validate.SingleOrDefault();
                if (updatephysicalLocation != null)
                {
                    updatephysicalLocation.PrimaryCategory_Id = primaryPhysicallocation.PrimaryID;
                    updatephysicalLocation.BusinessMustBeInDC = primaryPhysicallocation.BusinessMustbeinDC;
                    updatephysicalLocation.COFORequired = primaryPhysicallocation.CofORequired;
                    updatephysicalLocation.HOP_EHOPAllowed = primaryPhysicallocation.HOP_EHOPAllowed;
                    updatephysicalLocation.ExemptFromAllFees = primaryPhysicallocation.ExemptfromAllFees;
                    updatephysicalLocation.LicenseType = primaryPhysicallocation.LicenseType;
                    updatephysicalLocation.Status = primaryPhysicallocation.Status;
                }
                Update(updatephysicalLocation, updatephysicalLocation.RuleId);
                Save();
                result = "2";
            }
            return result;
        }
    }
}
