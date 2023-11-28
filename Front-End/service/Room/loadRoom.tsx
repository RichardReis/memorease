import { HomeDeckCardsData } from "../Home/loadHome";
import { GetRequest } from "../request";

export type RoomData = {
  roomId: number;
  roomName: string;
  roomCode: string;
  inReview: number;
  inLearning: number;
  totalCount: number;
  isAdmin: boolean;
  deckCards: HomeDeckCardsData[];
};

const LoadRoom = async (roomId: number): Promise<RoomData | null> => {
  let response = await GetRequest<RoomData>(`/Room/LoadRoom?roomId=${roomId}`);

  if (response.success) return response.object;

  return null;
};

export { LoadRoom };
