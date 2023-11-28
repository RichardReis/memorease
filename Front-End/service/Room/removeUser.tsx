import { PostRequest } from "../request";
import { RoomUserData } from "./addUser";

const RemoveUser = async (data: RoomUserData) => {
  let response = await PostRequest<any>("/Room/RemoveUser", data);

  return response.success;
};

export { RemoveUser };
