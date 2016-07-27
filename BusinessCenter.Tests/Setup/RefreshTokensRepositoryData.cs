using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
  public class RefreshTokensRepositoryData
    {
       private readonly List<RefreshTokens> _entities;
        public bool IsInitialized;

        public void AddRefreshTokensEntity(RefreshTokens obj)
        {
            _entities.Add(obj);
        }

        public List<RefreshTokens>RefreshTokensEntitiesList
        {
            get { return _entities; }
        }


        public RefreshTokensRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<RefreshTokens>();

            AddRefreshTokensEntity(new RefreshTokens()
            {
                Id = "4hFZuEDUmoAQPDe4EeTmMS8x/ecIxYRDatgyGnmMlLo=",
                Subject = "bharathpbk6699",
                ClientId = "352549070fb44ce793a5343a5f846dcc",
                IssuedUtc=Convert.ToDateTime("2016-01-20"),
                ExpiresUtc=Convert.ToDateTime("2016-01-20" ),
                ProtectedTicket = "OMhhXWxplzC47s9Kp4lThRg_W5IOxE4AxqOX0nfZozWKPIHDq3tX4X_V68zbq3UQjEyUaMWMKfLE5nkB2EA5ER14sJsTw0hMQo4JKaC8uADNKEhWUo-uGZsfYGARUD09an7MHmb6LnYbdsPpsPZ03P9pxNaR2O2pmmTxGNGaK86ruc_gP_wrmzcT2F-nEokTHCijXfmZRunA2QKwnx1KDGJsQBWJUsooispIkMcnTVKZnFa11YS5OUQRP7zMGd_MBxnbzAOSzUGXRO2BL1jbjBVp3fxZAcAv17ZQyszRybGR_oo2Fx0z9UrOhwE493LVgdpAg2eNQJ072Ezt2QeYwR-_xclr0a8mFF3cxpp8C0OmdCuzFNUDcfl7CCpe48oM9fYnCcTOAvxxvkSUKPKtsHQYNg0ULONYCJ1IFEfmRKQ"
            });
           
        }
    }
}
