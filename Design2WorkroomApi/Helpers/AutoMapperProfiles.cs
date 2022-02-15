using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // Automapper profiles 

            CreateMap<ClientDesignModel, ClientDesignDto>().ReverseMap();

            CreateMap<DesignerModel, DesignerDto>().ReverseMap();
            CreateMap<DesignerModel, DesignerCreateDto>().ReverseMap();
            CreateMap<DesignerModel, DesignerUpdateDto>().ReverseMap();

            CreateMap<ClientModel, ClientDto>().ReverseMap();
            CreateMap<ClientModel, ClientCreateDto>().ReverseMap();
            CreateMap<ClientModel, ClientUpdateDto>().ReverseMap();

            CreateMap<WorkroomModel, WorkroomDto>().ReverseMap();
            CreateMap<WorkroomModel, WorkroomCreateDto>().ReverseMap();
            CreateMap<WorkroomModel, WorkroomUpdateDto>().ReverseMap();

            CreateMap<ProfileModel, ProfileDto>().ReverseMap();
            CreateMap<ProfileModel, ProfileCreateDto>().ReverseMap();
            CreateMap<ProfileModel, ProfileUpdateDto>().ReverseMap();

            CreateMap<EmailModel, EmailDto>().ReverseMap();
            CreateMap<EmailModel, EmailCreateDto>().ReverseMap();
            CreateMap<EmailModel, EmailUpdateDto>().ReverseMap();

            CreateMap<DesignConceptModel, DesignConceptDto>().ReverseMap();
            CreateMap<DesignConceptModel, DesignConceptCreateDto>().ReverseMap();
            CreateMap<DesignConceptModel, DesignConceptUpdateDto>().ReverseMap();

            CreateMap<DesignConceptsApprovalModel, DesignConceptsApprovalsCreateDto>().ReverseMap();
            CreateMap<DesignConceptsApprovalModel, DesignConceptsApprovalsDto>().ReverseMap();

            CreateMap<WorkOrderModel, WorkOrderDto>().ReverseMap();
            CreateMap<WorkOrderModel, WorkOrderCreateDto>().ReverseMap();
            CreateMap<WorkOrderModel, WorkOrderUpdateDto>().ReverseMap();

            CreateMap<WorkOrderItemModel, WorkOrderItemDto>().ReverseMap();
            CreateMap<WorkOrderItemModel, WorkOrderItemCreateDto>().ReverseMap();
            CreateMap<WorkOrderItemModel, WorkOrderItemUpdateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<AttachmentsModel, AttachmentsDto>().ReverseMap();

            CreateMap<DesignerModel, ClientModel>().ReverseMap();
        }
    }
}
