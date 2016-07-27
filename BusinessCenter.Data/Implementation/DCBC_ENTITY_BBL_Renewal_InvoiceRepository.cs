using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class DCBC_ENTITY_BBL_Renewal_InvoiceRepository : GenericRepository<DCBC_ENTITY_BBL_Renewal_Invoice>, IDCBC_ENTITY_BBL_Renewal_InvoiceRepository
      
     
    {
        private readonly ILookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;
        public DCBC_ENTITY_BBL_Renewal_InvoiceRepository(IUnitOfWork context,ILookup_ExistingCategoriesRepository lookupExistingCategoriesRepository)
            : base(context)
        {
            _lookupExistingCategoriesRepository = lookupExistingCategoriesRepository;
        }
        /// <summary>
        /// This method is used to retrive Fee records, given LREN number
        /// </summary>
        /// <param name="lrennumber"></param>
        /// <returns>Invoice details</returns>
        public List<InvoiceModel> FindAmountByLicense(string lrennumber)
        {
            var IModel = new List<InvoiceModel>();
            var data = FindBySingle(x => x.b1_Alt_ID == lrennumber).ToList();
            foreach (var ilist in data)
            {
                InvoiceModel inmodel = new InvoiceModel();
                inmodel.GF_DES = ilist.GF_DES;
                inmodel.GF_FEE = Convert.ToDecimal(ilist.GF_FEE);
                inmodel.GF_UNIT = Convert.ToDecimal(ilist.GF_UNIT);
                IModel.Add(inmodel);
            }
            return IModel;
            
        }

        /// <summary>
        /// This method is used to Calculate the renewal fee, given LREN number
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Total Amount in decimal</returns>
        public decimal RenewalCalculation(RenewModel renewModel)
        {

            decimal subrenewtotal = 0;
            try
            {
                renewModel.EndorsementFee = 0;
                renewModel.ApplicationFee = 0;
                renewModel.LicenseAmount = 0;
                renewModel.LapsedAmount = 0;
                renewModel.ExpiredAmount = 0;
                var category = FindBySingle(x => x.b1_Alt_ID == renewModel.LrenNumber).ToList();
                
                if (category.Count() != 0)
                {
                    string data = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ENDORSEMENT).ToUpper();
                    var endoresement = category.Where(x => x.GF_DES.ToUpper().Trim().Contains(data)).ToList();
                    if (endoresement.Count() != 0)
                    {
                        var endoresementdata = endoresement.ToList();
                        if (endoresementdata.Count != 0)
                            renewModel.EndorsementFee = Convert.ToDecimal(endoresementdata.FirstOrDefault().GF_FEE);
                    }

                    string applicationstatic = (GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.APPLICATION)).ToUpper();
                   
                    var application = category.Where(x => x.GF_DES.ToUpper().Trim().Contains(applicationstatic)).ToList();
                    if (application.Count() != 0)
                    {
                        string raoFeeStatic =GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.RAOFEE).ToUpper();
                        var raofee = category.Where(x => x.GF_DES.ToUpper().Trim().Contains(raoFeeStatic)).ToList();
                        if (raofee.Count() != 0)
                        {
                            renewModel.ApplicationFee = Convert.ToDecimal(application.FirstOrDefault().GF_FEE)+ Convert.ToDecimal(raofee.FirstOrDefault().GF_FEE);
                            renewModel.RAOFee = Convert.ToDecimal(raofee.FirstOrDefault().GF_FEE);
                        }
                        else
                        {
                            renewModel.ApplicationFee = Convert.ToDecimal(application.FirstOrDefault().GF_FEE)+ 0;
                            renewModel.RAOFee = 0;
                        }
                    }
                   
                    //  General Business
                    var lookupCategoryName = _lookupExistingCategoriesRepository.FindBy(renewModel.CategoryName).ToList();
                    if (lookupCategoryName.Count() != 0)
                    {
                        renewModel.CategoryName = lookupCategoryName.FirstOrDefault().NewCategoryName;
                    }
                    var license = category.Where(x => x.GF_DES.ToUpper().Trim().Contains(renewModel.CategoryName.Trim().ToUpper())).ToList();
                    //     Application Fee
                    if (license.Count() != 0)
                    {
                        renewModel.LicenseAmount = Convert.ToDecimal(license.FirstOrDefault().GF_FEE);
                    }
                   
                   
                   
                    var subtotal = renewModel.LicenseAmount + renewModel.EndorsementFee +
                                   renewModel.ApplicationFee;
                    renewModel.TechFee = (subtotal * 10) / 100;

                    if (renewModel.Extradays.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.ApplicationValidateStatus.Lapsed).ToUpper())
                    {
                        renewModel.LapsedAmount = 250;
                        renewModel.ExpiredAmount = 0;

                    }
                    else if (renewModel.Extradays.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.ApplicationValidateStatus.Expired).ToUpper())
                    {
                        renewModel.LapsedAmount = 250;
                        renewModel.ExpiredAmount =250;
                    }
                    var bblpenaltyexipred = category.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).ToList();
                    if (bblpenaltyexipred.Any())
                    {
                        if (bblpenaltyexipred.Count() == 1)
                        {
                            category.Remove(
                                category.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).Single());
                        }
                        else
                        {
                            foreach (var penality in bblpenaltyexipred)
                            {
                                category.Remove(
                                 category.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)") && x.GF_FEE == penality.GF_FEE).Single());
                            }
                        }
                    }
                    var bblpenaltylapsed = category.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).ToList();
                    if (bblpenaltylapsed.Any())
                    {
                        if (bblpenaltylapsed.Count() == 1)
                        {
                            category.Remove(
                                category.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).Single());
                        }
                        else
                        {
                            foreach (var penality in bblpenaltylapsed)
                            {
                                category.Remove(
                                 category.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)") && x.GF_FEE == penality.GF_FEE).Single());
                            }
                        }
                    }

                    foreach (var renewfee in category)
                    {
                        subrenewtotal = subrenewtotal + Convert.ToDecimal(renewfee.GF_FEE);
                    }
                    renewModel.GrandTotalAmount = subrenewtotal + renewModel.ExpiredAmount+renewModel.LapsedAmount;
                }
                else
                {
                    renewModel.EndorsementFee = 0;
                    renewModel.ApplicationFee = 0;
                    renewModel.LicenseAmount = 0;
                    var subtotal = renewModel.LicenseAmount + renewModel.EndorsementFee +
                                   renewModel.ApplicationFee;
                    renewModel.TechFee = (subtotal * 10) / 100;
                    renewModel.GrandTotalAmount = renewModel.TechFee + subtotal;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return renewModel.GrandTotalAmount;
        }
    }
}
