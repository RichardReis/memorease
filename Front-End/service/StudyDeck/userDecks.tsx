import { GetRequest } from "../request";

export type UserDeckItem = {
  id: number;
  name: string;
  inReview: number;
  inLearning: number;
  studyRoomId?: number;
  createdAt: string;
};

export type UserDeckList = UserDeckItem[];

const UserDecks = async (): Promise<UserDeckList | null> => {
  let response = await GetRequest<UserDeckList>("/StudyDeck/UserDecks");

  if (response.success) return response.object;

  return null;
};

export { UserDecks };
