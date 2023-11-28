import { GetRequest } from "../request";

export type PrepareCardData = {
  id: number;
  studyDeckId: number;
  front: string;
  back: string;
};

const PrepareCard = async (
  studyCardId: number
): Promise<PrepareCardData | null> => {
  let response = await GetRequest<PrepareCardData>(
    `/StudyDeck/PrepareCard?studyCardId=${studyCardId}`
  );

  if (response.success) return response.object;

  return null;
};

export { PrepareCard };
