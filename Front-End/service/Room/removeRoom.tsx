import { DeleteRequest } from "../request";

const RemoveRoom = async (roomId: number) => {
  let response = await DeleteRequest<any>(`/Room/Remove?roomId=${roomId}`);

  return response.success;
};

export { RemoveRoom };
