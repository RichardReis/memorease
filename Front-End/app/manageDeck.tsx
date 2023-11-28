import { Stack, useLocalSearchParams, useRouter } from "expo-router";
import React, { useState, useEffect } from "react";
import { View, Text, StyleSheet, TouchableOpacity } from "react-native";
import DefaultScreenStructure from "../components/DefaultScreenStructure";
import InputText from "../components/Inputs/InputText";
import TextStyles from "../themedStyles/Text";
import Spacing from "../constants/Spacing";
import List from "../components/List";
import Colors from "../constants/Colors";
import Icon from "../components/Icon";
import Button from "../components/Button";
import DeleteModal from "../components/DeleteModal";
import { PrepareDeck, PrepareDeckData } from "../service/StudyDeck/prepareDeck";
import {
  DeckCardItem,
  DeckCardList,
  DeckCards,
} from "../service/StudyDeck/deckCards";
import { EditDeck, EditDeckType } from "../service/StudyDeck/editDeck";
import { RemoveCard } from "../service/StudyDeck/removeCard";

type CardType = {
  front: string;
  back: string;
};

const ManageDeck: React.FC = () => {
  const router = useRouter();
  const params = useLocalSearchParams();
  const { id } = params;

  const [deckName, setDeckName] = useState<string>("");
  const [deck, setDeck] = useState<PrepareDeckData | null>(null);
  const [listItems, setListItems] = useState<DeckCardList>([]);

  useEffect(() => {
    if (id) GetDeckInfo(id as string);
  }, [id]);

  const GetDeckInfo = async (id: string) => {
    let deckId = parseInt(id);
    let response = await PrepareDeck(deckId);
    if (!!response) {
      await GetDeckCards(response.id);
      setDeckName(response.name);
    }
    setDeck(response);
  };

  const GetDeckCards = async (id: number) => {
    let response = await DeckCards(id);
    if (!!response) setListItems(response);
  };

  useEffect(() => {
    if (!!deckName && !!deck) {
      if (deckName != deck.name) {
        Save(deck, deckName);
      }
    }
  }, [deckName]);

  const Save = async (oldData: PrepareDeckData, name: string) => {
    let data = {
      id: oldData.id,
      name: name,
      userId: oldData.userId,
      studyRoomId: oldData.studyRoomId,
    } as EditDeckType;

    let response = await EditDeck(data);
  };

  const Card = ({ front, back, id, studyDeckId }: DeckCardItem) => {
    const [flip, setFlip] = useState<boolean>(false);

    const DeleteCard = async () => {
      let response = await RemoveCard(id);
      if (response) GetDeckCards(studyDeckId);
    };

    return (
      <View style={flip ? { ...styles.card, ...styles.cardFlip } : styles.card}>
        {!flip ? (
          <View>
            <Text
              style={{
                ...TextStyles.labelBold,
                color: Colors["light"].primary,
                fontSize: 14,
              }}
            >
              Frente
            </Text>
            <Text style={{ ...TextStyles.label, fontSize: 18 }}>{front}</Text>
          </View>
        ) : (
          <View>
            <Text
              style={{
                ...TextStyles.labelBold,
                color: Colors["light"].white,
                fontSize: 14,
              }}
            >
              Verso
            </Text>
            <Text
              style={{
                ...TextStyles.label,
                color: Colors["light"].white,
                fontSize: 18,
              }}
            >
              {back}
            </Text>
          </View>
        )}
        <View style={{ flexDirection: "row", justifyContent: "center" }}>
          <TouchableOpacity
            style={styles.button}
            onPress={() => setFlip(!flip)}
          >
            <Icon color={Colors["light"].success} name="rotate-right" />
          </TouchableOpacity>
          <TouchableOpacity
            style={styles.button}
            onPress={() => router.push("/createCard")}
          >
            <Icon color={Colors["light"].text} name="note-edit" />
          </TouchableOpacity>
          <View style={styles.button}>
            <DeleteModal
              title={`Deletar Cartão`}
              text={front}
              onConfirm={DeleteCard}
              returnHref="/manageDeck"
            />
          </View>
        </View>
      </View>
    );
  };

  if (!deck) return <></>;
  return (
    <>
      <Stack.Screen options={{ headerShown: false }} />
      <DefaultScreenStructure title={"Gerenciar Baralho"} activeBackButton>
        <View style={{ flex: 1 }}>
          <View style={{ padding: Spacing.g }}>
            <Text style={{ ...TextStyles.label }}>Nome do Baralho</Text>
            <InputText
              value={deckName}
              onChange={(text) => setDeckName(text)}
            />
          </View>
          <View
            style={{
              padding: Spacing.g,
              flexDirection: "row",
              justifyContent: "space-between",
              alignItems: "baseline",
            }}
          >
            <Text style={{ ...TextStyles.subtitle }}>Cartões</Text>
            <Button
              type="primary"
              icon="plus"
              onPress={() => router.push("/createCard")}
            />
          </View>
          <List data={listItems} render={(item) => <Card {...item} />} />
        </View>
      </DefaultScreenStructure>
    </>
  );
};

const styles = StyleSheet.create({
  card: {
    padding: 16,
    justifyContent: "space-between",
    backgroundColor: Colors["light"].contentBackground,

    marginHorizontal: Spacing.m,
    marginBottom: Spacing.m,

    borderRadius: Spacing.m,

    borderColor: Colors["light"].primary,
    borderBottomWidth: 3,

    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.23,
    shadowRadius: 2.62,

    elevation: 4,
  },
  cardFlip: {
    backgroundColor: Colors["light"].primary,

    borderColor: Colors["light"].contentBackground,
  },
  button: {
    width: 60,
    height: 60,

    alignItems: "center",
    justifyContent: "center",
  },
});

export default ManageDeck;
