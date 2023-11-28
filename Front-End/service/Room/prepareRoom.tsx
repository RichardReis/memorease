import { GetRequest } from "../request";
import { RoomItemData } from "./loadRooms";

const PrepareRoom = async (roomId: number): Promise<RoomItemData | null> => {
  let response = await GetRequest<RoomItemData>(
    `/Room/Prepare?roomId=${roomId}`
  );

  if (response.success) return response.object;

  return null;
};

export { PrepareRoom };
