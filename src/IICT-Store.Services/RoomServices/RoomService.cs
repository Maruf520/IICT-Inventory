using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using IICT_Store.Dtos.Rooms;
using IICT_Store.Models;
using IICT_Store.Repositories.RoomRepositories;
using IICT_Store.Repositories.TestRepo;

namespace IICT_Store.Services.RoomServices
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;
        private readonly IBaseRepo baseRepo;

        public RoomService(IRoomRepository roomRepository, IMapper mapper, IBaseRepo baseRepo)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.baseRepo = baseRepo;
        }

        public ServiceResponse<GetRoomDto> CreateRoom(CreateRoomDto createRoomDto, string userId)
        {
            ServiceResponse<GetRoomDto> response = new();
            try
            {
                var rooms = roomRepository.GetAll();
                if (rooms.Any(x => x.RoomNo == createRoomDto.RoomNo))
                {
                    response.SetMessage(
                        new List<string> { new string($"{createRoomDto.RoomNo}, This room no already exits.") },
                        HttpStatusCode.BadRequest);
                    return response;
                }

                Room room = new Room()
                {
                    RoomNo = createRoomDto.RoomNo,
                    RoomType = createRoomDto.RoomType,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId

                };
                roomRepository.Insert(room);
                /*                System.Net.Mail.MailMessage mailMessage = new();
                                MailMessage message = new MailMessage(
                         "iict.sust.edu@gamil.com",
                         "md.maruf5201@gmail.com",
                         "Quarterly data report.",
                         "See the attached spreadsheet.");
                                SendEmailx(message);*/
                var roomToReturn = mapper.Map<GetRoomDto>(room);
                response.Data = roomToReturn;
                response.SetMessage(new List<string> { new("Room Created.") }, HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                response.SetMessage(new List<string> { new(e.Message) }, HttpStatusCode.InternalServerError);
                return response;
            }
        }
        public ServiceResponse<GetRoomDto> UpdateRoom(CreateRoomDto createRoomDto, int roomId, string userId)
        {
            ServiceResponse<GetRoomDto> response = new();
            try
            {
                var rooms = roomRepository.GetById(roomId);
                if (rooms == null)
                {
                    response.SetMessage(
                        new List<string> { new string($"{createRoomDto.RoomNo}Room Not Found") },
                        HttpStatusCode.BadRequest);
                    return response;
                }

                rooms.RoomNo = createRoomDto.RoomNo;
                rooms.RoomType = createRoomDto.RoomType;
                rooms.UpdatedAt = DateTime.Now;
                rooms.UpdatedBy = userId;
                roomRepository.Update(rooms);
                var roomToReturn = mapper.Map<GetRoomDto>(rooms);
                response.Data = roomToReturn;
                response.SetMessage(new List<string> { new("Room Updated.") }, HttpStatusCode.OK);
                return response;
            }
            catch (Exception e)
            {
                response.SetMessage(new List<string> { new(e.Message) }, HttpStatusCode.InternalServerError);
                return response;
            }
        }
        public ServiceResponse<GetRoomDto> GetById(int id)
        {
            ServiceResponse<GetRoomDto> response = new();
            var room = roomRepository.GetById(id);
            if (room == null)
            {
                response.SetMessage(new List<string>() { new string("Room Not Found.") }, HttpStatusCode.NotFound);
                return response;
            }
            var roomToReturn = mapper.Map<GetRoomDto>(room);
            response.Data = roomToReturn;
            return response;
        }

        public ServiceResponse<List<GetRoomDto>> GetAllRoom()
        {
            ServiceResponse<List<GetRoomDto>> response = new();
            var rooms = roomRepository.GetAll().ToList();
            if (!rooms.Any())
            {
                response.SetMessage(new List<string>() { new string("No Rooms Found.") }, HttpStatusCode.OK);
                return response;
            }
            var roomToReturn = mapper.Map<List<GetRoomDto>>(rooms);
            response.Data = roomToReturn;
            return response;
        }

        public ServiceResponse<GetRoomDto> Delete(int id)
        {
            ServiceResponse<GetRoomDto> response = new();
            var room = roomRepository.GetById(id);
            if (room == null)
            {
                response.SetMessage(new List<string>() { new string("Room Not Found.") });
                return response;
            }

            roomRepository.Delete(id);
            response.SetMessage(new List<string>() { new string("Room Deleted") }, HttpStatusCode.OK);
            return response;
        }/*

        public static void SendEmailx(System.Net.Mail.MailMessage m)
        {
            SendEmail(m, true);
        }



        public static void SendEmail(System.Net.Mail.MailMessage m, Boolean Async)
        {
            System.Net.Mail.SmtpClient smtpClient = null;
            smtpClient = new System.Net.Mail.SmtpClient();

            NetworkCredential credential = new();
            credential.Password = "MahiKeya0124";
            credential.Domain = "iict.sust.edu@gmail.com";

            if (Async)
            {
                SendEmailDelegate sd = new SendEmailDelegate(smtpClient.Send);
                AsyncCallback cb = new AsyncCallback(SendEmailResponse);
                sd.BeginInvoke(m, cb, sd);
            }
            else
            {
                smtpClient.Send(m);
            }
        }

        private delegate void SendEmailDelegate(System.Net.Mail.MailMessage m);
        private static void SendEmailResponse(IAsyncResult ar)
        {
            SendEmailDelegate sd = (SendEmailDelegate)(ar.AsyncState);

            sd.EndInvoke(ar);
        }*/
    }
}