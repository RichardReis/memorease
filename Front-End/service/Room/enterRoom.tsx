import { GetRequest } from "../request";
import { RoomItemData } from "./loadRooms";

const EnterRoom = async (roomCode: string) => {
  let response = await GetRequest<RoomItemData>(
    `/Room/EnterRoom?roomCode=${roomCode}`
  );

  if (response.success) return response.success;

  return false;
};

export { EnterRoom };
