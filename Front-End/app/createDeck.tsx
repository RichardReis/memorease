import React from "react";

import { BuildInputsType } from "../components/Form/BuildInputs";
import FormScreenStructure from "../components/FormScreenStructure";
import * as StudyDeck from "../service/StudyDeck/createDeck";
import { useRouter } from "expo-router";

const CreateDeck: React.FC = () => {
  const router = useRouter();
  const inputs: BuildInputsType = [
    {
      name: "name",
      label: "Nome do Baralho",
      required: true,
      type: "text",
    },
  ];

  const create = async (data: any) => {
    let response = await StudyDeck.CreateDeck(data);
    if (response) router.push("/studyDeckList");
  };

  return (
    <FormScreenStructure
      title="Criar Novo Baralho"
      inputs={inputs}
      buttonText="Salvar"
      onSubmit={create}
    />
  );
};

export default CreateDeck;
