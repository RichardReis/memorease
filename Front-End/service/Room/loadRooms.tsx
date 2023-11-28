import { GetRequest } from "../request";

export type RoomItemData = {
  id: number;
  name: string;
  code: number;
  adminId: string;
  isPublic: boolean;
};

export type RoomDataList = RoomItemData[];

const LoadRooms = async (): Promise<RoomDataList | null> => {
  let response = await GetRequest<RoomDataList>("/Room/LoadRooms");

  if (response.success) return response.object;

  return null;
};

export { LoadRooms };
