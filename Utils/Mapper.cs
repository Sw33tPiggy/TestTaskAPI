using System.Collections.Generic;
using APITest.Domain.Models;
using APITest.Resources;
using System.Linq;

namespace APITest.Utils {
    public static class Mapper {
        public static IEnumerable<UserResource> UsersToUserResources(IEnumerable<User> users){
            IEnumerable<UserResource> userResources = users.Select( i => UserToUserResource(i));
            return userResources;
        }

        private static UserResource UserToUserResource(User user){
            return new UserResource{
                Id = user.Id, 
                Name = user.GivenName + " " + user.Surname
            };
        }

        public static AccessTokenResource AccessTokenResourceToAccessToken(AccessToken accessToken){
            return new AccessTokenResource{
                AccessToken = accessToken.Token,
                Expiration = accessToken.Expiration
            };
        }

        public static FileResource FileRecordToFileResource(FileRecord fileRecord){
            return new FileResource{
                Id = fileRecord.Id,
                Name = fileRecord.Name + " " + fileRecord.Type
            };
        }

        public static IEnumerable<FileResource> FileRecordsToFileResources(IEnumerable<FileRecord> fileRecords){
            IEnumerable<FileResource> fileResources = fileRecords.Select( i => FileRecordToFileResource(i));
            return fileResources;
        }
    }
}