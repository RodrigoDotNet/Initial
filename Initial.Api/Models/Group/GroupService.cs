using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Initial.Api.Resources;
using Initial.Api.Util;
using System;

namespace Initial.Api.Models
{
    public partial class GroupService
        : PrivateService<Group, GroupRequest, GroupResponse>, IGroupService
    {
        public GroupService(IGroupRepository repository, AppSettings appSettings)
            : base(repository, appSettings) { }

        protected override GroupResponse Parse(Group model)
        {
            return new GroupResponse
            {
                Id = model.Id,
                Name = model.Name,
                EntityVersion = model.LastModifiedDate
            };
        }

        protected override Group Parse(AccountTicket user, GroupRequest request)
        {
            return new Group
            {
                Name = request.Name,
                EnterpriseId = user.EnterpriseId,

                CreationDate = DateTime.Now,
                CreationUserId = user.Id,
                LastModifiedDate = DateTime.Now,
                LastModifiedUserId = user.Id
            };
        }

        protected override void Merge(AccountTicket user, Group model, GroupRequest request)
        {
            model.Name = request.Name;
            model.LastModifiedDate = DateTime.Now;
            model.LastModifiedUserId = user.Id;
        }

        protected override void AuditDelete(AccountTicket user, Group model)
        {
            model.Inactive = true;
            model.LastModifiedDate = DateTime.Now;
            model.LastModifiedUserId = user.Id;
        }

        protected override bool Validate
            (AccountTicket user, GroupRequest request)
        {
            return base.Validate(user, request);
        }

        protected override bool Validate
            (AccountTicket user, GroupRequest request, Group model)
        {
            if (model.Id > 0)
            {
                if (model.EnterpriseId == null)
                {
                    State.Add("", Messages.EnterpriseId_ReadOnly);

                    return false;
                }

                if (model.EnterpriseId != user.EnterpriseId)
                {
                    State.Add("", Messages.EnterpriseId_Invalid);

                    return false;
                }

                if (request.EntityVersion != null
                    && request.EntityVersion != model.LastModifiedDate)
                {
                    State.Add("EntityVersion", Messages.EntityVersion_Conflict);

                    return false;
                }
            }

            return base.Validate(user, request, model);
        }
    }
}
