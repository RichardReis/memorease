import React, { useState, useEffect } from "react";

import { BuildInputsType } from "../components/Form/BuildInputs";
import FormScreenStructure from "../components/FormScreenStructure";
import * as StudyDeck from "../service/StudyDeck/addCard";
import { useLocalSearchParams, useRouter } from "expo-router";
import { PrepareCard, PrepareCardData } from "../service/StudyDeck/prepareCard";
import { EditCard } from "../service/StudyDeck/editCard";

const CreateCard: React.FC = () => {
  const router = useRouter();
  const params = useLocalSearchParams();
  const { id, deckId } = params;

  const [card, setCard] = useState<PrepareCardData | null>(null);

  const inputs: BuildInputsType = [
    {
      name: "front",
      label: "Frente",
      required: true,
      type: "text",
    },
    {
      name: "back",
      label: "Verso",
      required: true,
      type: "text",
    },
  ];

  useEffect(() => {
    if (!!id) {
      getCard(parseInt(id as string));
    }
  }, [id]);

  const getCard = async (id: number) => {
    let response = await PrepareCard(id);
    setCard(response);
  };

  const save = async (data: any) => {
    let response = false;
    if (!!card) {
      response = await EditCard({
        ...data,
        id: card.id,
        studyDeckId: card.studyDeckId,
      });
    } else {
      response = await StudyDeck.AddCard({
        ...data,
        id: 0,
        studyDeckId: deckId,
      });
    }
    if (response)
      router.push({
        pathname: "/manageDeck",
        params: {
          id: deckId,
        },
      });
  };

  if (!deckId) return <></>;
  return (
    <FormScreenStructure
      title="Adicionar Carta"
      inputs={inputs}
      buttonText="Salvar"
      onSubmit={save}
    />
  );
};

export default CreateCard;
