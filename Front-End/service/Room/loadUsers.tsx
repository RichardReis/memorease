import { GetRequest } from "../request";

export type UserRoomItem = {
  id: string;
  firstName: string;
  name: string;
  email: string;
};

export type UserRoomList = UserRoomItem[];

const LoadUsers = async (roomId: number): Promise<UserRoomList | null> => {
  let response = await GetRequest<UserRoomList>(
    `/Room/LoadUsers?roomId=${roomId}`
  );

  if (response.success) return response.object;

  return null;
};

export { LoadUsers };
