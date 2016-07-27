USE [TestBusinessCenter]
GO

/****** Object:  View [dbo].[BblLicenseView2]    Script Date: 12/30/2015 5:20:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO















ALTER view [dbo].[BblLicenseView2] as
SELECT       distinct  SM.SubmissionLicense as Application,

(select distinct [dbo].[fn_PrimaryCategory](sc.CategoryTypeID,'PRIMARY',SM.MasterId) where  [dbo].[fn_PrimaryCategory](sc.CategoryTypeID,'PRIMARY',SM.MasterId) !='') as [Primary Category],
						
					
(select '') as Class,						

(select  [dbo].[fn_Quantity](SM.MasterId,'PRIMARY'))AS [Pri. Units],


(SELECT        MPC.UnitOne
FROM            MasterPrimaryCategory MPC where  MPC.PrimaryID = (select CategoryTypeID  from SubmissionCategory subc where subc.MasterId=SM.MasterId and subc.CategoryType='PRIMARY'))AS [Pri. Units Description],
                     



	( select  [dbo].[fn_QuantityTwo](SM.MasterId,'PRIMARY'))AS [Pri. Units 2],


	(SELECT        MPC.UnitTwo
FROM            MasterPrimaryCategory MPC where  MPC.PrimaryID = (select CategoryTypeID  from SubmissionCategory subc where subc.MasterId=SM.MasterId and subc.CategoryType='PRIMARY'))AS [Pri. Units Description 2],
                     



					   
							( select  [dbo].fn_getStringValue(SM.MasterId,'SECONDARYCATEGORY'))AS [Secondary Category],

							   --  CASE WHEN sc.CategoryType = 'SECONDARYCATEGORY' THEN
							   --(SELECT        CASE WHEN sc.ItemQty LIKE '%,%' THEN LEFT(sc.ItemQty, Charindex(',', sc.ItemQty) - 1) else sc.ItemQty END AS Expr1)
							   --end AS [Sec. Units],
							    ( select    [dbo].[fn_SecondaryQuantity] (sc.MasterId,'SECONDARYCATEGORY')) [Sec. Units],
							  
							   ( select   [dbo].[fn_ViewTwoSecDescription](sc.MasterId,'SECONDARYCATEGORY')) AS [Sec. Units Description],
							  

							  (select SubCategoryName from MasterSubCategory where SubCatID in (select CategoryTypeID from SubmissionCategory where MasterId=SM.MasterId and CategoryType='SUBCATEGORY')) as SubCategoryName

							  --(SELECT        UnitOne
         --                      FROM            MasterPrimaryCategory
         --                      WHERE        MasterPrimaryCategory.Description =(select SecondaryLicenseCategory from MasterSecondaryLicenseCategory where SecondaryID= sc.categorytypeid) and sc.CategoryType = 'SECONDARYCATEGORY')  AS [Sec. Units Description]
                             
FROM            SubmissionCategory AS sc INNER JOIN
                         SubmissionMaster SM ON sc.MasterId = SM.MasterId INNER JOIN
                         UserBBLService ON SM.SubmissionLicense = UserBBLService.DCBC_ENTITY_ID where (SM.Status = 'UnderReview')AND (UserBBLService.Type = 'S')













GO


