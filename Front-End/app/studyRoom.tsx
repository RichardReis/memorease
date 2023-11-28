import React, { useState, useEffect } from "react";
import {
  StyleSheet,
  TouchableOpacity,
  Text,
  ScrollView,
  View,
} from "react-native";
import * as Clipboard from "expo-clipboard";
import { Stack, useLocalSearchParams, useRouter } from "expo-router";
import TextStyles from "../themedStyles/Text";
import DefaultScreenStructure from "../components/DefaultScreenStructure";
import Icon from "../components/Icon";
import List from "../components/List";
import { DeckCard } from "../components/DeckCard";
import Spacing from "../constants/Spacing";
import { HomeCardColorStyles } from "../themedStyles/HomeCard";
import { UserDeckList, UserDecks } from "../service/StudyDeck/userDecks";
import { LoadRoom, RoomData } from "../service/Room/loadRoom";

const StudyRoom: React.FC = () => {
  const params = useLocalSearchParams();
  const { roomId } = params;

  const [data, setData] = useState<RoomData | null>(null);
  const [carouselItems, setCarouselItems] = useState<UserDeckList>([]);

  useEffect(() => {
    Load();
  }, []);

  const Load = async () => {
    let response = await LoadRoom(parseInt(roomId as string));
    if (response) {
      setData(response);
      GetList();
    }
  };

  const GetList = async () => {
    let response = await UserDecks();
    if (response) setCarouselItems(response);
  };

  const copyToClipboard = async (text: string) => {
    await Clipboard.setStringAsync(text);
  };

  const Card = ({
    text,
    type,
  }: {
    text: string;
    type: "success" | "neutral" | "danger";
  }) => {
    return (
      <View style={{ ...HomeCardColorStyles[type], ...styles.statisticCard }}>
        <Text style={styles.statisticText}>{text}</Text>
      </View>
    );
  };

  if (!data) return <></>;
  return (
    <>
      <Stack.Screen options={{ headerShown: false }} />
      <DefaultScreenStructure
        activeBackButton
        title={data.roomName}
        headerButton={
          <TouchableOpacity
            style={styles.clipboard}
            onPress={() => copyToClipboard(data.roomCode)}
          >
            <Icon name="content-copy" color="white" />
            <Text
              style={{ ...TextStyles.clipboard }}
            >{`  ${data.roomCode}`}</Text>
          </TouchableOpacity>
        }
      >
        <View style={styles.statisticContent}>
          <ScrollView horizontal={true} showsHorizontalScrollIndicator={false}>
            <Card text={`A revisar ${data.inReview}`} type="success" />
            <Card text={`Em Aprendizado ${data.inLearning}`} type="neutral" />
            <Card text={`Total Pendentes ${data.totalCount}`} type="danger" />
          </ScrollView>
        </View>
        <List data={carouselItems} render={(item) => <DeckCard {...item} />} />
      </DefaultScreenStructure>
    </>
  );
};

const styles = StyleSheet.create({
  clipboard: {
    flexDirection: "row",
    alignItems: "center",
  },
  statisticContent: {
    marginBottom: Spacing.g,
  },
  statisticCard: {
    padding: Spacing.m,
    borderRadius: Spacing.m,
    marginHorizontal: 12,
  },
  statisticText: {},
});

export default StudyRoom;
