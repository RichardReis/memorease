import React, { useEffect, useState } from "react";
import { Dimensions, StyleSheet, Text, TouchableOpacity } from "react-native";
import { View } from "react-native";
import Icon from "../../components/Icon";
import ButtonStyles from "../../themedStyles/Button";
import TextStyles from "../../themedStyles/Text";
import Spacing from "../../constants/Spacing";
import Colors from "../../constants/Colors";
import Button from "../../components/Button";
import { useRouter } from "expo-router";
import Modal from "react-native-modal";
import DeleteModal from "../DeleteModal";
import { UserDeckItem } from "../../service/StudyDeck/userDecks";
import { RemoveDeck } from "../../service/StudyDeck/removeDeck";
import { DeckInfo, LoadStudyData } from "../../service/StudyDeck/deckInfo";

type DeckCardProps = {
  title: string;
};

const ITEM_WIDTH = Dimensions.get("window").width * 0.88;

export const DeckCard = ({
  id,
  name,
  inLearning,
  inReview,
  createdAt,
}: UserDeckItem) => {
  const router = useRouter();
  const [openModal, setOpenModal] = useState<boolean>(false);
  const [statistic, setStatistic] = useState<LoadStudyData | null>(null);

  const ToggleModal = async () => {
    if (!openModal && !statistic) await GetStatistic();
    setOpenModal(!openModal);
  };

  const Delete = async () => {
    let response = await RemoveDeck(id);
  };

  const GetStatistic = async () => {
    let response = await DeckInfo(id);
    setStatistic(response);
  };

  const ModalStatistic = () => {
    if (!statistic) return <></>;
    return (
      <Modal isVisible={openModal} onBackdropPress={ToggleModal}>
        <View
          style={{
            backgroundColor: Colors["light"].contentBackground,
            padding: Spacing.g,
            borderRadius: Spacing.g,
          }}
        >
          <Text style={{ ...TextStyles.regular, fontSize: 20 }}>
            Este Baralho possui
          </Text>

          <View style={{ flexDirection: "row", paddingVertical: Spacing.s }}>
            <Text>
              <Icon name="chart-line" color={Colors["light"].success} />
            </Text>
            <Text
              style={{
                ...TextStyles.regular,
                fontSize: 18,
                borderBottomWidth: 1,
                flex: 1,
                marginLeft: Spacing.s,
              }}
            >
              Uma Media de Desempenho de{" "}
              <Text
                style={{
                  ...TextStyles.labelBold,
                  fontSize: 24,
                  color: Colors["light"].success,
                }}
              >
                {statistic.performance}
              </Text>
            </Text>
          </View>

          <View style={{ flexDirection: "row", paddingVertical: Spacing.s }}>
            <Text>
              <Icon name="card" color={Colors["light"].neutral} />
            </Text>
            <Text
              style={{
                ...TextStyles.regular,
                fontSize: 18,
                borderBottomWidth: 1,
                flex: 1,
                marginLeft: Spacing.s,
              }}
            >
              Com{" "}
              <Text
                style={{
                  ...TextStyles.labelBold,
                  fontSize: 24,
                  color: Colors["light"].neutral,
                }}
              >
                {statistic.cardsStudied}
              </Text>{" "}
              Cartões Estudados
            </Text>
          </View>

          <View style={{ flexDirection: "row", paddingVertical: Spacing.s }}>
            <Text>
              <Icon name="account-group" color={Colors["light"].primary} />{" "}
            </Text>
            <Text
              style={{
                ...TextStyles.regular,
                fontSize: 18,
                borderBottomWidth: 1,
                flex: 1,
                marginLeft: Spacing.s,
              }}
            >
              E{" "}
              <Text
                style={{
                  ...TextStyles.labelBold,
                  fontSize: 24,
                  color: Colors["light"].primary,
                }}
              >
                {1}
              </Text>{" "}
              Usuarios Estudando{" "}
            </Text>
          </View>
          <View style={{ marginTop: Spacing.m }}>
            <Button title="Fechar" type="primary" onPress={ToggleModal} />
          </View>
        </View>
      </Modal>
    );
  };

  return (
    <View style={styles.cardView}>
      <View style={styles.card}>
        <View style={{ width: "100%" }}>
          <View style={styles.cardHeader}>
            <Text
              style={{
                ...TextStyles.deckTitle,
                flex: 1,
                color: Colors["light"].primary,
              }}
            >
              {name}
            </Text>
            <DeleteModal
              returnHref="/studyDeckList"
              text={name}
              title={`Deletar Baralho`}
              onConfirm={Delete}
            />
          </View>
          <View style={{ borderBottomWidth: 1 }} />
          <Text style={{ ...TextStyles.regular }}>Criado em: {createdAt}</Text>
        </View>
        <View style={{ marginVertical: Spacing.m }}>
          <Text style={{ ...TextStyles.label, fontSize: 20 }}>
            A revisar: {inReview}
          </Text>
          <Text style={{ ...TextStyles.label, fontSize: 20 }}>
            Em Aprendizado: {inLearning}
          </Text>
          <View style={styles.buttongroup}>
            <Button
              type="primary"
              icon="square-edit-outline"
              onPress={() =>
                router.push({
                  pathname: "/manageDeck",
                  params: {
                    id: id,
                  },
                })
              }
            />
            <Button type="primary" icon="chart-bar" onPress={ToggleModal} />
            <Button
              type="success"
              icon="play"
              size={70}
              onPress={() =>
                router.push({
                  pathname: "/studyingDeck",
                  params: {
                    deckId: id,
                  },
                })
              }
            />
          </View>
        </View>
      </View>
      <ModalStatistic />
    </View>
  );
};

const styles = StyleSheet.create({
  cardView: {
    flex: 1,
    justifyContent: "space-between",
  },
  card: {
    width: ITEM_WIDTH,
    justifyContent: "space-between",

    backgroundColor: Colors["light"].contentBackground,
    borderRadius: 8,

    marginBottom: Spacing.g,
    marginHorizontal: Spacing.g,

    padding: Spacing.m,

    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.23,
    shadowRadius: 2.62,

    elevation: 4,

    borderRightWidth: 3,
    borderBottomWidth: 3,
    borderColor: Colors["light"].primary,
  },
  cardHeader: {
    flexDirection: "row",
    alignItems: "flex-start",
    justifyContent: "space-between",
  },
  buttongroup: {
    flexDirection: "row",
    justifyContent: "flex-end",
    alignItems: "flex-end",
    gap: Spacing.s,

    marginTop: Spacing.m,
  },
  button: {
    ...ButtonStyles.primary,
    width: 50,
    borderRadius: 50,

    alignItems: "center",
    justifyContent: "center",
  },
});
