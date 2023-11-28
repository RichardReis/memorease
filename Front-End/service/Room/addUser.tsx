import { PostRequest } from "../request";

export type RoomUserData = {
  roomId: number;
  email: string;
};

const AddUser = async (data: RoomUserData) => {
  let response = await PostRequest<any>("/Room/AddUser", data);

  return response.success;
};

export { AddUser };
