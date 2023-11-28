import { PostRequest } from "../request";
import { RoomItemData } from "./loadRooms";

const CreateRoom = async (data: RoomItemData) => {
  let response = await PostRequest<any>("/Room/Create", data);

  return response.success;
};

export { CreateRoom };
