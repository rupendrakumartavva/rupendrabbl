USE [TestBusinessCenter]
GO

/****** Object:  View [dbo].[BblLicenseView4]    Script Date: 12/30/2015 5:22:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









ALTER view [dbo].[BblLicenseView4]
as
SELECT        SubmissionMaster.SubmissionLicense as [Application/License No.], (CASE SubmissionMaster.DocSubmType  WHEN 'IN' THEN 'InPerson'  WHEN 'ON' THEN 'OnLine'  ELSE '' END) DocumentSubmissionType,
                             (SELECT        Description
                               FROM            MasterPrimaryCategory
                               WHERE        (PrimaryID IN
                                                             (SELECT        CategoryTypeID
                                                               FROM            SubmissionCategory
                                                               WHERE        (MasterId = SubmissionMaster.MasterId) AND (CategoryType = 'PRIMARY')))) AS CategoryName, SubmissionDocument.MasterCategoryDocId, 
                       --  MasterCategoryDocument.CategoryName,
						  MasterCategoryDocument.Agency, MasterCategoryDocument.Agency_FullName, MasterCategoryDocument.Div, MasterCategoryDocument.DivisionFullName, 
                         MasterCategoryDocument.SupportingDocuments,SubmissionDocument.FileLocation as SubmissionFileLocation,SubmissionDocument.FileName as SubmissionFileName
						 , MasterCategoryDocument.ShortDocName, MasterCategoryDocument.Description
FROM            SubmissionMaster INNER JOIN
                         SubmissionDocument ON SubmissionMaster.MasterId = SubmissionDocument.MasterId INNER JOIN
                         MasterCategoryDocument ON SubmissionDocument.MasterCategoryDocId = MasterCategoryDocument.MasterCategoryDocId
WHERE        (SubmissionMaster.Status = 'UnderReview')












GO


