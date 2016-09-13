/*
 * MeetupManager:
 * Copyright (C) 2013 Refractored LLC: 
 * http://github.com/JamesMontemagno
 * http://twitter.com/JamesMontemagno
 * http://refractored.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Models.Database;
using MeetupManager.Portable.Services.Database;
using Xamarin.Forms;


[assembly:Dependency(typeof(DataService))]
namespace MeetupManager.Portable.Services.Database
{
  /// <summary>
  /// Is the main data service that can be used in the application for reading/writing to the cameroon database.
  /// </summary>
	public class DataService : IDataService
  {
    private readonly MeetupManagerDatabase database;
    public DataService()
    {
      this.database = new MeetupManagerDatabase();
    }

    #region IDataService implementation

    public async Task CheckInMember(EventRSVP rsvp)
    {
      await Task.Factory.StartNew(() =>
      {
        database.SaveItem<EventRSVP>(rsvp);
      });
    }

    public async Task<bool> IsCheckedIn(string eventId, string userId, string eventName, string groupId, string groupName, long eventDate)
    {
      return await Task.Factory.StartNew<bool>(() =>
      {
        var e = database.GetEventRSVP(eventId, userId);
        if (e != null && string.IsNullOrEmpty(e.GroupId))
        {
          e.EventName = eventName;
          e.GroupId = groupId;
          e.GroupName = groupName;
          e.EventDate = eventDate;
          database.SaveItem<EventRSVP>(e);
        }
        return e != null;
      });
    }

    public Task CheckOutMember(string eventId, string userId)
    {
      return Task.Factory.StartNew(() =>
      {
        var item = database.GetEventRSVP(eventId, userId);
        if (item != null)
          database.DeleteItem<EventRSVP>(item);
      });
    }
    #endregion





    public Task AddNewMember(NewMember member)
    {
      return Task.Factory.StartNew(() =>
      {
        database.SaveItem<NewMember>(member);
      });
    }

    public Task<IEnumerable<NewMember>> GetNewMembers(string eventId)
    {
      return Task.Factory.StartNew(() => database.GetNewMembers(eventId));
    }


    public Task RemoveNewMember(int id)
    {
      return Task.Factory.StartNew(() => database.DeleteItem<NewMember>(id));
    }

		public Task<IEnumerable<NewMember>> GetNewMembersForGroup (string groupId)
		{
			return Task.Factory.StartNew(() => database.GetNewMembersByDate(groupId));
		}

		public Task<IEnumerable<EventRSVP>> GetRSVPsForGroup (string groupId)
		{
			return Task.Factory.StartNew(() => database.GetRSVPsByDate(groupId));
		}
  }
}
