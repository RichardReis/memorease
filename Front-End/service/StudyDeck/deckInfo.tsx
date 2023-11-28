import { GetRequest } from "../request";

type CardInfoData = {
  front: string;
  performance: string;
  repetition: number;
};

export type LoadStudyData = {
  performance: string;
  cardsStudied: number;
  cardInfo: CardInfoData[];
};

const DeckInfo = async (deckid: number): Promise<LoadStudyData | null> => {
  let response = await GetRequest<LoadStudyData>(
    `/StudyDeck/DeckInfo?deckid=${deckid}`
  );

  if (response.success) return response.object;

  return null;
};

export { DeckInfo };
