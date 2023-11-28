import { PostRequest } from "../request";
import { RoomItemData } from "./loadRooms";

const EditRoom = async (data: RoomItemData) => {
  let response = await PostRequest<any>("/Room/Edit", data);

  return response.success;
};

export { EditRoom };
