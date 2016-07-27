USE [TestBusinessCenter]
GO

/****** Object:  View [dbo].[BblLicenseView3]    Script Date: 12/30/2015 5:22:27 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER view  [dbo].[BblLicenseView3] as

SELECT DISTINCT  

--(select b1_Alt_ID from DCBC_ENTITY_BBL_Renewals where License_Being_Renewed in(select top 1 B1_ALT_ID from DCBC_ENTITY_BBL where DCBC_ENTITY_ID=(CAST(SubmissionMaster.SubmissionLicense AS int))
-- ))as[Renewal License No.],

(select top 1 b1_Alt_ID from DCBC_ENTITY_BBL_Renewals where License_Being_Renewed in (select top 1 B1_ALT_ID from DCBC_ENTITY_BBL where DCBC_ENTITY_ID=(CAST(SubmissionMaster.SubmissionLicense AS int)))) as[Renewal License No.],
                             (SELECT        Description
                               FROM            MasterPrimaryCategory
                               WHERE        (PrimaryID IN
                                                             (SELECT        CategoryTypeID
                                                               FROM            SubmissionCategory
                                                               WHERE        (MasterId = SubmissionMaster.MasterId) AND (CategoryType = 'PRIMARY')))) AS [Primary Category],  SubmissionMaster.LicenseDuration as [License Renewal Period], 
                  (select GF_FEE from [dbo].[DCBC_ENTITY_BBL_Renewal_Invoice] where GF_DES='Application Fee' and b1_Alt_ID in (select top 1  b1_Alt_ID  from DCBC_ENTITY_BBL_Renewals where License_Being_Renewed=DCBC_ENTITY_BBL.B1_ALT_ID)) as [ApplicationFee],
                   (select GF_FEE from [dbo].[DCBC_ENTITY_BBL_Renewal_Invoice] where GF_DES='Endorsement Fee' and b1_Alt_ID in (select top 1  b1_Alt_ID  from DCBC_ENTITY_BBL_Renewals where License_Being_Renewed=DCBC_ENTITY_BBL.B1_ALT_ID)) as [Endorsement Fee],
				   (select GF_FEE from [dbo].[DCBC_ENTITY_BBL_Renewal_Invoice] where GF_DES=DCBC_ENTITY_BBL.B1_PER_CATEGORY and b1_Alt_ID in (select top 1  b1_Alt_ID  from DCBC_ENTITY_BBL_Renewals where License_Being_Renewed=DCBC_ENTITY_BBL.B1_ALT_ID)) as LicenseFee,
				   (select GF_FEE from [dbo].[DCBC_ENTITY_BBL_Renewal_Invoice] where GF_DES='RAO Fee' and b1_Alt_ID in (select top 1  b1_Alt_ID  from DCBC_ENTITY_BBL_Renewals where License_Being_Renewed=DCBC_ENTITY_BBL.B1_ALT_ID)) as [RAO Fee],
                (select GF_FEE from [dbo].[DCBC_ENTITY_BBL_Renewal_Invoice] where GF_DES='Enhanced Service Fee of 10%' and b1_Alt_ID in (select top 1  b1_Alt_ID  from DCBC_ENTITY_BBL_Renewals where License_Being_Renewed=DCBC_ENTITY_BBL.B1_ALT_ID)) as [ESFFee],
                
                
                 SubmissionMaster.GrandTotal as [TotalAmount],
				  
                 (select top 1 TranscationId from [dbo].[PaymentDetails] where MasterId=SubmissionMaster.MasterId) as [Payment Transaction ID]
                  
FROM            SubmissionMaster INNER JOIN
                         UserBBLService ON SubmissionMaster.SubmissionLicense = UserBBLService.DCBC_ENTITY_ID INNER JOIN
                         DCBC_ENTITY_BBL ON UserBBLService.DCBC_ENTITY_ID = DCBC_ENTITY_BBL.DCBC_ENTITY_ID
WHERE        (SubmissionMaster.Status = 'UnderReview') AND (UserBBLService.Type = 'A')







GO


