import React, { useState, useEffect } from "react";
import Button from "../../components/Button";
import ListScreenStructure from "../../components/ListScreenStructure";
import { useRouter } from "expo-router";
import { DeckCard } from "../../components/DeckCard";
import { UserDeckList, UserDecks } from "../../service/StudyDeck/userDecks";

const StudyDeckList: React.FC = () => {
  const router = useRouter();
  const [carouselItems, setCarouselItems] = useState<UserDeckList>([]);

  useEffect(() => {
    GetList();
  }, []);

  const GetList = async () => {
    let response = await UserDecks();
    if (response) setCarouselItems(response);
  };

  return (
    <ListScreenStructure
      title="Baralhos"
      headerButton={
        <Button
          type="secondary"
          icon="plus"
          title="Criar"
          onPress={() => router.push("/createDeck")}
        />
      }
      listData={carouselItems}
      listRender={(item) => <DeckCard {...item} />}
    />
  );
};

export default StudyDeckList;
