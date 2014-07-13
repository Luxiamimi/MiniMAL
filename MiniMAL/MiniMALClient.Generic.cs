using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using MiniMAL.Exceptions;
using MiniMAL.Internal;
using MiniMAL.Internal.Interfaces;

namespace MiniMAL
{
    public partial class MiniMALClient
    {
        private Task<TUserList> LoadUserListAsync<TUserList>() where TUserList : IUserList, new()
        {
            if (IsConnected)
                return LoadUserListAsync<TUserList>(ClientData.Username);
            throw new UserNotConnectedException();
        }

        static private async Task<TUserList> LoadUserListAsync<TUserList>(string user)
            where TUserList : IUserList, new()
        {
            string link = RequestLink.UserList<TUserList>(user);
            XmlDocument xml = await LoadXmlAsync(link);

            var list = new TUserList();
            list.LoadFromXml(xml);
            return list;
        }

        private async Task<ListRequestResult> AddEntryAsync<TRequestData>(int id, TRequestData data)
            where TRequestData : IRequestData, new()
        {
            string link = RequestLink.AddEntry<TRequestData>(id);
            var requestData = new Dictionary<string, string> {{"data", data.SerializeToString()}};

            try
            {
                await RequestAsync(link, requestData);
            }
            catch (RequestException e)
            {
                if (e.Message.IndexOf("already", StringComparison.OrdinalIgnoreCase) >= 0)
                    return ListRequestResult.AlreadyInTheList;

                throw;
            }

            return ListRequestResult.Created;
        }

        private async Task<ListRequestResult> UpdateEntryAsync<TRequestData>(int id, TRequestData data)
            where TRequestData : IRequestData, new()
        {
            string link = RequestLink.UpdateEntry<TRequestData>(id);
            var requestData = new Dictionary<string, string> { { "data", data.SerializeToString() } };

            await RequestAsync(link, requestData);

            return ListRequestResult.Updated;
        }

        private async Task<TSearchResult> SearchAsync<TSearchResult>(string[] search)
            where TSearchResult : ISearchResult, new()
        {
            string link = RequestLink.Search<TSearchResult>(search);
            XmlDocument xml = await RequestXmlAsync(link);

            var result = new TSearchResult();
            result.LoadFromXml(xml);
            return result;
        }
    }
}