import { GetRequest } from "../request";

export type HomeDeckCardsData = {
  id: number;
  name: string;
  inReview: number;
  inLearning: number;
};

type HomeRoomCardsData = {
  id: number;
  name: string;
  code: number;
  isAdmin: boolean;
};

export type LoadHomeData = {
  userFirstName: string;
  inReview: number;
  inLearning: number;
  totalCount: number;
  deckCards: HomeDeckCardsData[];
  roomCards: HomeRoomCardsData[];
};

const LoadHome = async (): Promise<LoadHomeData | null> => {
  let response = await GetRequest<LoadHomeData>(`/StudyDeck/LoadHome`);

  if (response.success) return response.object;

  return null;
};

export { LoadHome };
