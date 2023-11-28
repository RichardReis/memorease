import { GetRequest } from "../request";

export type DeckCardItem = {
  id: number;
  studyDeckId: number;
  front: string;
  back: string;
};

export type DeckCardList = DeckCardItem[];

const DeckCards = async (studyDeckId: number): Promise<DeckCardList | null> => {
  let response = await GetRequest<DeckCardList>(
    `/StudyDeck/DeckCards?studyDeckId=${studyDeckId}`
  );

  if (response.success) return response.object;

  return null;
};

export { DeckCards };
