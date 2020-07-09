using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Aliyun.Acs.Core.Auth;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Regions;
using Aliyun.Acs.Core.Regions.Endpoints;
using Aliyun.Acs.Core.Regions.Location;

namespace Aliyun.Acs.Core.Profile
{
    public class MultiProfile : IClientProfile
    {
        private static ConcurrentDictionary<string, MultiProfile> _profiles = new ConcurrentDictionary<string, MultiProfile>();

        private Credential credential;
        private readonly EndpointResolve endpointResolve;
        private readonly ICredentialProvider iCredentialProvider;
        private readonly string regionId;
        public FormatType acceptFormat;
        private LocationConfig locationConfig;

        public static MultiProfile GetProfile(string regionId, string accessKeyId, string secret)
        {
            string key = regionId + "_" + accessKeyId;
            MultiProfile profile;
            if (!_profiles.TryGetValue(key, out profile))
            {
                var credential = new Credential(accessKeyId, secret);
                profile = new MultiProfile(regionId, credential);
                _profiles.TryAdd(key, profile);
            }

            return profile;
        }

        private MultiProfile()
        {
            endpointResolve = new EndpointResolve();
            locationConfig = new LocationConfig();
        }

        private MultiProfile(string region, Credential creden) : this()
        {
            credential = creden;
            regionId = region;
        }

        public string DefaultClientName { get; set; }

        public void AddEndpoint(string endpointName, string regionId, string product, string domain, bool isNeverExpire = false)
        {
            EndpointUserConfig.AddEndpoint(product, regionId, domain);
            endpointResolve.AddEndpoint(endpointName, regionId, product, domain, isNeverExpire);
        }

        public Credential GetCredential()
        {
            if (null == credential && null != iCredentialProvider)
            {
                credential = iCredentialProvider.Fresh();
            }

            return credential;
        }

        public List<Endpoint> GetEndpoints(string product, string regionId, string serviceCode, string endpointType)
        {
            return endpointResolve.Resolve(product, regionId, serviceCode, endpointType, credential, locationConfig);
        }

        public List<Endpoint> GetEndpoints(string regionId, string product)
        {
            return endpointResolve.GetEndpoints(regionId, product);
        }

        public FormatType GetFormat()
        {
            return acceptFormat;
        }

        public string GetRegionId()
        {
            return regionId;
        }

        [Obsolete]
        public ISigner GetSigner()
        {
            return null;
        }

        public void SetCredentialsProvider(AlibabaCloudCredentialsProvider credentialsProvider)
        {
            if (credential != null)
            {
                return;
            }

            credential = new CredentialsBackupCompatibilityAdaptor(credentialsProvider);
        }

        public void SetLocationConfig(string regionId, string product, string endpoint)
        {
            locationConfig = LocationConfig.createLocationConfig(regionId, product, endpoint);
        }
    }
}
