﻿using Advertising.Common.Interfaces;
using Advertising.Common.Models;
using Grpc.Core;
using Spaces.Grpc.Protos;

namespace Advertising.Api.Services
{
    public sealed class AdvertisingServices : IAdvertisingService
    {
        private readonly SpaceProtoService.SpaceProtoServiceClient spaceProtoServiceClient;

        public AdvertisingServices(SpaceProtoService.SpaceProtoServiceClient spaceProtoServiceClient)
        {
            this.spaceProtoServiceClient = spaceProtoServiceClient;
        }

        #region IAdvertisingService Members

        public async Task<IEnumerable<AdSpace>> GetAllAsync()
        {
            var request = new GetSpacesRequest();

            GetSpacesResponse getSpacesResponse = await this.spaceProtoServiceClient.GetSpacesAsync(request);

            var adSpaces = new List<AdSpace>();

            foreach(var space in getSpacesResponse.Spaces)
                adSpaces.Add(new AdSpace { Name = space.Name, Address = space.Address , Description=space.Description, Image=space.Image, IsFree=space.Isfree});

            return adSpaces;
        }

        public async Task<bool> AddAsync(AdSpaceInfo adSpaceInfo)
        {

            try
            {
                var spaceInfo = new SpaceInfo { Name = adSpaceInfo.Name, Address = adSpaceInfo.Address, Description = adSpaceInfo.Description, Image = adSpaceInfo.Image, Isfree = adSpaceInfo.IsFree };
                var request = new InsertSpaceRequest
                {
                    Space = spaceInfo
                };
                InsertSpaceResponse insertSpaceResponse = await this.spaceProtoServiceClient.InsertSpaceAsync(request);

                return insertSpaceResponse.Response;
            }
            catch (RpcException ex)
            {
                
                return false; 
            }

        }

        #endregion
    }
}
