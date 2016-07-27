USE [BusinessCenter]
GO

/****** Object:  View [dbo].[BblLicenseView]    Script Date: 12/28/2015 21:06:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









ALTER view [dbo].[BblLicenseView]
as
SELECT    SubmissionMaster.Status as [Online App License Status],
    SubmissionMaster.SubmissionLicense as [Application/License No.],
   (SELECT Description FROM MasterPrimaryCategory WHERE (PrimaryID IN (SELECT CategoryTypeID FROM SubmissionCategory WHERE (MasterId = SubmissionMaster.MasterId) 
   AND (CategoryType = 'PRIMARY')))) AS [Application Type],SubmissionMaster.App_Type, SubmissionMaster.LicenseDuration as [License Period], 
                         SubmissionMaster.ApplicationFee as [ApplicationFee], 
                             (SELECT        SUM(EndorsementFee) AS Expr1
                               FROM            SubmissionCategory AS SubmissionCategory_2
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [EndorsementFee],
							  
                             (SELECT        SUM(LicenseCategoryFee) AS Expr1
                               FROM            SubmissionCategory AS SubmissionCategory_1
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [LicenseFee],
							    SubmissionMaster.RAOFee,
							   (select ((SUM(EndorsementFee)+SUM(LicenseCategoryFee)+(SubmissionMaster.ApplicationFee)+(SubmissionMaster.RAOFee)))*0.1 FROM          
							     SubmissionCategory AS SubmissionCategory_1 WHERE    (MasterId = SubmissionMaster.MasterId)) AS [ESFFee],
								 SubmissionMaster.eHOP,
                           SubmissionMaster.GrandTotal as TotalAmount,

                             (SELECT        TOP (1) TranscationId
                               FROM            PaymentDetails
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Payment Transaction ID],

							    (SELECT        TOP (1) PaymentDate
                               FROM            PaymentDetails
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Payment Transaction Date],


							   (select  TOP (1) [FullName] from SubmissionTaxRevenue where MasterId=SubmissionMaster.MasterId) as [CH Self Certificate Signature],

							    (select  TOP (1)  CreatdedDate from SubmissionTaxRevenue where MasterId=SubmissionMaster.MasterId) as [CH Self Certificate Date],

								(select  TOP (1)  TaxRevenueType from SubmissionTaxRevenue where MasterId=SubmissionMaster.MasterId) as [CH Self Certificate Type],

								(select  TOP (1)  FullName from SubmissionSelfCertification where MasterId=SubmissionMaster.MasterId) as [Self Certificate (One-Family)],

							   --Payment Transaction Date

							     (select Number from SubmissionCofo_Hop_Ehop where MasterId=SubmissionMaster.MasterId and UserSelectType='cofo') as [Certificate of Occupancy Number],
							 (select DateOfIssuance from SubmissionCofo_Hop_Ehop where MasterId=SubmissionMaster.MasterId and UserSelectType='cofo') as [CO Issue Date ],
                            

							   (select Number from SubmissionCofo_Hop_Ehop where MasterId=SubmissionMaster.MasterId and UserSelectType='hop') as [Home Occupation Number],
							 (select DateOfIssuance from SubmissionCofo_Hop_Ehop where MasterId=SubmissionMaster.MasterId and UserSelectType='hop') as [HO Issue Date],

                        --    (select IsValid from SubmissionCofo_Hop_Ehop where MasterId=SubmissionMaster.MasterId and UserSelectType='ehop') as [EHOP Issuance Status],
						  (select Number from SubmissionCofo_Hop_Ehop where MasterId=SubmissionMaster.MasterId and UserSelectType='ehop') as [EHOP Number],
							 (select DateOfIssuance from SubmissionCofo_Hop_Ehop where MasterId=SubmissionMaster.MasterId and UserSelectType='ehop') as [EHOP Issue Date],
                        	 


							 (SELECT        TOP (1)  [User].FirstName +''+ [User].LastName
FROM            [User] INNER JOIN
                         SubmissionEHOPEligibility ON [User].Id = SubmissionEHOPEligibility.CreatedBy where SubmissionEHOPEligibility.MasterId=SubmissionMaster.MasterId) as [SEHOP Attested By],


						 (SELECT        TOP (1)  MasterEhopOptionType.EhopOptionName
FROM            MasterEhopOptionType INNER JOIN
                         SubmissionEHOPEligibility ON MasterEhopOptionType.EhopOptionId = SubmissionEHOPEligibility.TypeId where SubmissionEHOPEligibility.MasterId=SubmissionMaster.MasterId) as [EHOP Occupation Type],

							(SELECT CASE(select IsValid from SubmissionCofo_Hop_Ehop where MasterId=SubmissionMaster.MasterId and UserSelectType='ehop') WHEN 0 THEN 'Issued' WHEN 1 THEN '' END) as [EHOP Issuance Status],
		 
		                           
							   (SELECT        FirstName
                               FROM            SubmissionCorporation_Agent_Address
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [First Name],
							
                             (SELECT        MiddelName
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_10
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [Middle Name],
                             (SELECT        LastName
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_9
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [Last Name],
														 (SELECT        BusinessName
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_11
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [Organization Name],
                             (SELECT        Address1
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_8
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [Full Address Line 1],
                             (SELECT        Address2
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_7
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [Address Line 2],                          
                             (SELECT        City
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_5
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [City],
                             (SELECT        State
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_4
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [State],
														     (SELECT        ZipCode
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_2
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [Zip Code],
                             (SELECT        Country
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_3
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS [Country/Region],
                         
                             (SELECT        Email
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_1
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPREG')) AS Email, 
														 SubmissionMaster.BusinessStructure as [Business Organization***],
														  SubmissionMaster.TradeName as [Trade Name (If applicable)],

														( select isnull(STRNum.TaxRevenueNumber,'') as [FEIN-TaxRevenueNumber] from SubmissionTaxRevenue STRNum where STRNum.MasterId=SubmissionMaster.MasterId and STRNum.TaxRevenueType='SSIN') as SSN,
														( select isnull(STRNum.TaxRevenueNumber,'') as [FEIN-TaxRevenueNumber] from SubmissionTaxRevenue STRNum where STRNum.MasterId=SubmissionMaster.MasterId and STRNum.TaxRevenueType='FEIN') as FEIN,
														  	   --(SELECT        TaxRevenueNumber
                    --           FROM            SubmissionTaxRevenue
                    --           WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Social Security Nbr (SSN)],
							
                             


														     (SELECT        AddressNumber
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_7
                               WHERE         (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Prim.Street Number],
								
														     (SELECT        StreetName
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_7
                               WHERE         (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Prim.Street Name],

														     (SELECT        StreetType
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_7
                               WHERE         (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS StreetType,

		


                         
                             (SELECT        Quadrant
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_6
                               WHERE         (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS Quadrant,
                             (SELECT        UnitType
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_5
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Unit Type***],
														    (SELECT        Unit
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_5
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Suite/Unit Number],
                             (SELECT        City
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_4
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS PCity,
                             (SELECT        State
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_3
                               WHERE         (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS PState,
                             (SELECT        Country
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_2
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Country**],
                             (SELECT        Zip
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_1
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [PZip],
													
							   (SELECT        Telephone
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_1
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Phone],



 (SELECT        AddressNumberSufix
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_1
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Parcel.Premise Suffix],

 (SELECT        Ward
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_1
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Parcel.Premise Ward],
(SELECT        Anc
                               FROM            SubmissionCofo_Hop_Ehop_Address AS SubmissionBusinessAddress_1
                               WHERE        (CustomTypeId=SubmissionLocationandStruc_1.SubCofoHopEhopId and((CustomType = 'cofo') OR
                                                         (CustomType = 'hop') OR
                                                         (CustomType = 'ehop')))) AS [Parcel.Premise ANC],



														 (Select BusinessName from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Org/Buss. Name],
									 (Select ContactFirstName from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.ContactFirstName],
									 (Select ContactMiddleName from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.ContactMiddleName],	
									 (Select ContactLastName from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.ContactLastName],	
								
								 (Select StreetNumber from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Street Number],
									 (Select StreetName from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Street Name],	
									 (Select StreetType from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Street Type],	
									
									 (Select Quadrant from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Quadrant],	
									 (Select UnitNumber from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Suite/Unit Number],	
										 								
								--(Select FullAddress from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as Mailing_address,
							   
							   (Select [City] from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as[Billing.City],	
							   (Select [State] from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.State],	
							   (Select [Country] from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Country***],	
							    (Select [zip] from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Zip],	

							   (Select [ContactNumber1] from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Phone],

							   (Select [EmailAddress] from [dbo].[PaymentAddressDetails] where PaymentAddressDetails.PaymentId=PaymentDetails_1.PaymentId) as [Billing.Email],
							   
							   
							     (SELECT        BusinessName
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_8
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Agent.Org/Buss. Name],
  (SELECT        FirstName
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_8
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Contact First Name],
  (SELECT        MiddelName
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_8
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Contact Middle Name],
  (SELECT        LastName
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_8
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Contact Last Name],

							   (SELECT        Address1
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_8
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [StreetFullAddress],
                             (SELECT        Address2
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_7
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Street Name],
   (SELECT        Address3
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_7
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Street Type],
							 
							  (SELECT        Quadrant
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_7
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Agent.Quadrant],
							  
							  (SELECT        UnitNumber
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_7
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Agent.UnitNumber],


                             (SELECT        City
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_5
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPREG' OR
                                                         AddressType = 'N-CORPAGENT')) AS ContactAgent_City,
                             (SELECT        State
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_4
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS ContactAgent_State,
                             (SELECT        Country
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_3
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS ContactAgent_Country,
                             (SELECT        ZipCode
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_2
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS ContactAgent_ZipCode,


    (SELECT        Telephone
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_1
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS [Corp.Telephone],
							   




                             (SELECT        Email
                               FROM            SubmissionCorporation_Agent_Address AS SubmissionCorpAgent_1
                               WHERE        (SubCorporationRegId = SubmissionCorporation_Agent.SubCorporationRegId) AND (AddressType = 'Y-CORPAGENT' OR
                                                         AddressType = 'N-CORPAGENT')) AS ContactAgent_Email,
							   
							   
							   
							   
							   
							   
							   						   
							  
                              SubmissionCorporation_Agent.FileNumber,

                             (SELECT      top 1  CompanyName
                               FROM            SubmissionIndividual
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Company Name],
                             (SELECT     top 1   CompanyBusinessLicense
                               FROM            SubmissionIndividual AS SubmissionIndividual_13
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Company Buss. Lic Number],
                             --(SELECT        FirstName
                             --  FROM            SubmissionIndividual AS SubmissionIndividual_12
                             --  WHERE        (MasterId = SubmissionMaster.MasterId)) AS IndividualFirstName,
                             --(SELECT        MiddleName
                             --  FROM            SubmissionIndividual AS SubmissionIndividual_11
                             --  WHERE        (MasterId = SubmissionMaster.MasterId)) AS IndividualMiddleName,
                             --(SELECT        LastName
                             --  FROM            SubmissionIndividual AS SubmissionIndividual_10
                             --  WHERE        (MasterId = SubmissionMaster.MasterId)) AS IndividualLastName,
                             (SELECT        top 1 DateofBirth
                               FROM            SubmissionIndividual AS SubmissionIndividual_9
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Date of Birth],
                             (SELECT      top 1   City + ' , ' + [State/Province] + ',' + Country AS Expr1
                               FROM            SubmissionIndividual AS SubmissionIndividual_8
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Place of Birth],
                             (SELECT      top 1   Height
                               FROM            SubmissionIndividual AS SubmissionIndividual_7
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS Height,
                             (SELECT      top 1   Weight
                               FROM            SubmissionIndividual AS SubmissionIndividual_6
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Weight],
                             (SELECT      top 1   HairColor
                               FROM            SubmissionIndividual AS SubmissionIndividual_5
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Hair Color],
                             (SELECT      top 1   EyeColor
                               FROM            SubmissionIndividual AS SubmissionIndividual_4
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Eyes Color],
                             (SELECT        top 1 IdentificationCard
                               FROM            SubmissionIndividual AS SubmissionIndividual_3
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [Drivers License],
                             (SELECT      top 1   StateofIssuance
                               FROM            SubmissionIndividual AS SubmissionIndividual_2
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS [State of License],
                             (SELECT     top 1    ExpirationDate
                               FROM            SubmissionIndividual AS SubmissionIndividual_1
                               WHERE        (MasterId = SubmissionMaster.MasterId)) AS IndividualExpirationDate

							

				   
FROM            SubmissionMaster INNER JOIN
                         SubmissionCorporation_Agent ON SubmissionMaster.MasterId = SubmissionCorporation_Agent.MasterId INNER JOIN
                         SubmissionCofo_Hop_Ehop AS SubmissionLocationandStruc_1 ON SubmissionMaster.MasterId = SubmissionLocationandStruc_1.MasterId INNER JOIN
                         PaymentDetails AS PaymentDetails_1 ON SubmissionMaster.MasterId = PaymentDetails_1.MasterId
						  INNER JOIN
                         UserBBLService ON SubmissionMaster.SubmissionLicense = UserBBLService.DCBC_ENTITY_ID 
WHERE        (SubmissionMaster.Status = 'UnderReview')AND (UserBBLService.Type = 'S')




















GO


