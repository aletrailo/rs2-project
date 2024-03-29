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

            foreach (var space in getSpacesResponse.Spaces)
                adSpaces.Add(new AdSpace { SpaceId = space.Id, Name = space.Name, Address = space.Address, Description = space.Description, Image = space.Image, IsFree = space.Isfree, PricePerHour = space.Priceperhour, Owner = space.Owner });

            return adSpaces;
        }

        public async Task<bool> AddAsync(AdSpaceInfo adSpaceInfo)
        {

            try
            {
                var spaceInfo = new SpaceInfo { Name = adSpaceInfo.Name, Address = adSpaceInfo.Address, Description = adSpaceInfo.Description, Image = adSpaceInfo.Image, Isfree = adSpaceInfo.IsFree, Priceperhour = adSpaceInfo.PricePerHour, Owner = adSpaceInfo.Owner };
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

        public async Task<bool> BookASpaceAsync(UsernameSpaceIdInfo usernameSpaceIdInfo)
        {
            try
            {
                var reservation = new UsernameSpaceId { Spaceid = usernameSpaceIdInfo.spaceId, Username = usernameSpaceIdInfo.username };
                var request = new BookASpaceRequest
                {
                    Reservation = reservation
                };
                BookASpaceResponse bookASpaceResponse = await this.spaceProtoServiceClient.BookASpaceAsync(request);

                return bookASpaceResponse.Response;

            } catch (RpcException ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAdAsync(UsernameSpaceIdInfo usernameSpaceIdInfo)
        {
            try
            {
                var deleteInfo = new UsernameSpaceId { Spaceid = usernameSpaceIdInfo.spaceId, Username = usernameSpaceIdInfo.username };
                var request = new DeleteAdRequest
                {
                    Deleteinfo = deleteInfo
                };
                DeleteAdResponse deleteAdResponse = await this.spaceProtoServiceClient.DeleteAdAsync(request);

                return deleteAdResponse.Response;

            }
            catch (RpcException ex)
            {
                return false;
            }
        }

        public async Task<bool> EndUpUsingSpaceAsync(string spaceId)
        {
            try
            {
                var request = new EndUpUsingSpaceRequest
                {
                    Spaceid = spaceId
                };
                EndUpUsingSpaceResponse endUpUsingSpaceResponse = await this.spaceProtoServiceClient.EndUpUsingSpaceAsync(request);

                return endUpUsingSpaceResponse.Response;
            }
            catch (RpcException ex)
            {
                return false;
            }



        }
        #endregion
    }
}
