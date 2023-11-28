import { GetRequest } from "../request";

export type PrepareDeckData = {
  id: number;
  name: string;
  userId: string;
  studyRoomId?: number;
};

const PrepareDeck = async (
  studyDeckId: number
): Promise<PrepareDeckData | null> => {
  let response = await GetRequest<PrepareDeckData>(
    `/StudyDeck/Prepare?studyDeckId=${studyDeckId}`
  );

  if (response.success) return response.object;

  return null;
};

export { PrepareDeck };
